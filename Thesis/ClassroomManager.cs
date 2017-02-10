using SQLite;
using System;
using System.Collections.Generic;
using Thesis.Table;
using System.Timers;
using System.IO;

namespace Thesis
{
    public class ClassroomManager
    {
        //Attendance
        private DateTime todaysDate;
        private DateTime timeStarted;
        //Attendance Timer
        private Timer timer;
        public int timeCounter = 0;
        public bool activateIsLate = false;
        private int lateIn = 5;
        //Objects
        private Teacher _teacher;
        // the only instance of the teacher and 
        //responsible for the retrieval of student and subject data to list
        private List<Student> _allStudents; //all registered students in the app
        private List<Subject> _teachersSubjects; //all registered subjects in the app; 
        private Subject _currentActiveSubject; //TO BE ADDED
        //private List<Student> _activeStudents; //students who joined the class
        private List<Student> _subjectStudents; //students who are enrolled in a subject
        private bool classroomIsActive = false; //active is when the teacher starts the server
        //FOR QUIZ


        //instantiate the classroom class after the authentication of the teacher
        public ClassroomManager(Teacher teacher)
        {
            DBManager.init(teacher);
            _teacher = teacher;
            //getting the list of the teacher's subjects
            _teachersSubjects = _teacher.AllSubjects;
            //getting the list of the teacher's students and add a subject for it
            Subject allstudentssubject = new Subject(0, "All Students", _teacher.GetID);
            //allstudentssubject.RegisteredStudents = _teacher.AllStudents;
            _teachersSubjects.Add(allstudentssubject);
            _currentActiveSubject = allstudentssubject;
   
            //instanting empty list for students in a subject
            _subjectStudents = new List<Student>();
            //_currentActiveSubject = null;
        }
        //Properties
        public bool ClassroomIsActive {
            get { return classroomIsActive; }
            set { classroomIsActive = value; }
        }
        public Teacher GetTeacher { get { return _teacher; } }
        public List<Subject> GetSubjects { get { return _teachersSubjects; } }
        public List<Student> GetTeachersStudents { get { return _allStudents; } }

        public List<Student> GetSubjectStudents
        {
            get {  return _currentActiveSubject.RegisteredStudents;   }
        }

        public void ToggleInThisSubject(Student selectedStudent)
        {
            DBManager.ToggleStudentInASubject(selectedStudent);
        }

        //------------------------active--------------------------//
        public bool StartClass(string ipaddress, int lateIn)
        {
            if(ServerController.FireUp(ipaddress))
            {
                this.lateIn = lateIn;
               ClassroomIsActive = true;

                //Initializing Attendance
                todaysDate = DateTime.Now;
                timeStarted = DateTime.Now;
                //Initializing Attandance Timer
                 timer = new Timer(60000);
                // Hook up the Elapsed event for the timer.
                timer.Elapsed += OnTimedEvent;
                timer.Enabled = true;
                timer.Start();
                SaveAttendanceToCSV();
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<string> GetAllQuizzes()
        {
            string folderlocation;
            if(Android.OS.Environment.ExternalStorageState.Equals(Android.OS.Environment.MediaMounted))
                folderlocation = Android.OS.Environment.ExternalStorageDirectory.Path;
            else
                folderlocation = Android.OS.Environment.DirectoryDocuments;

            folderlocation += @"/Quizzes/";
            if(!Directory.Exists(folderlocation))
                Directory.CreateDirectory(folderlocation);

            // string file = @"/*.dat";

            string filepath = folderlocation;
            //DirectoryInfo d = new DirectoryInfo(folderlocation);
            string[] files = Directory.GetFiles(folderlocation);
            var quiznames = new List<string>();
            if(files != null)
            {
                foreach(var file in files)
                {
                    FileInfo fi = new FileInfo(file);
                    //  fi.Name.Replace(fi.Extension, "");
                    quiznames.Add(fi.Name.Replace(fi.Extension, ""));
                }
            }
            return quiznames;
        }
        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            timeCounter++;
           // Console.WriteLine("Counter:{}", timeCounter);
            if(timeCounter == lateIn)
            {
                timer.Stop();
                activateIsLate = true;
              
                timeCounter = 0;
                timer.Enabled = false;
                ServerController.isLate = activateIsLate;
               // Console.WriteLine("Status:{}", attendancestatus);
            }
        }
        // retrieving current subject

        public Subject CurrentSubject
        {
            get { return _currentActiveSubject; }
            set { _currentActiveSubject = value; }
        }
        public void GetStudentsInASubject(int subject_id) {     }

        public void RegisterUnregisteredStudents() {  }
   
        public void SaveAttendanceToCSV()
        {
            string folderlocation;
            if(Android.OS.Environment.ExternalStorageState.Equals(Android.OS.Environment.MediaMounted))
                folderlocation = Android.OS.Environment.ExternalStorageDirectory.Path;
            else
                folderlocation = Android.OS.Environment.DirectoryDocuments;

            folderlocation += @"/PICAttendance";
            if(!Directory.Exists(folderlocation))
                Directory.CreateDirectory(folderlocation);

            StreamWriter writer;
            string file = @"/" + todaysDate.ToString("yyyy-MM-dd") + "_" + timeStarted.ToString("h-mm-tt") + "_" +CurrentSubject + ".csv";
            using(writer = new StreamWriter(folderlocation + file , false))
            {
                writer.WriteLine("DATE: "+todaysDate);
                writer.WriteLine("Subject: "+CurrentSubject);
                writer.WriteLine("Teacher: "+_teacher.GetFullName);
                writer.WriteLine();
                writer.WriteLine("ID,Student Name,Status");
                int id = 1;
                foreach(var student in GetSubjectStudents)
                {
                    string fullname = student.GetFirstName + " " + student.GetLastName;
                    string status;
                    switch(student.Status)
                    {
                        case 2:
                            status = "Present";
                            break;
                        case 3:
                            status = "Late";
                            break;
                        default:
                            status = "Absent";
                            break;
                    }
                    writer.WriteLine("{0},{1},{2}", id++, fullname, status);
                }
            }
        }
    
        //------------------------Inactive/Active--------------------------//

        public void DeleteStudent(Student student)
        {
            _teacher.DeleteStudent(student);
            GetSubjectStudents.Remove(student);
        }
        public void DeleteSubject(Subject subject)
        {
            _teacher.DeleteSubject(subject);
        }
        public void AddStudent(Student student)
        {
            _teacher.AddStudent(student);
        }
        public void AddSubject(Subject subject)
        {
            _teacher.AddSubject(subject);
        }       
    }
}