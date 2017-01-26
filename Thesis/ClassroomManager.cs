using SQLite;
using System;
using System.Collections.Generic;
using Thesis.Table;
namespace Thesis
{
    public class ClassroomManager
    {
        private Teacher _teacher;
        // the only instance of the teacher and 
        //responsible for the retrieval of student and subject data to list
        private List<Student> _allStudents; //all registered students in the app
        private List<Subject> _teachersSubjects; //all registered subjects in the app; 

        private Subject _currentActiveSubject; //TO BE ADDED
        private List<Student> _activeStudents; //students who joined the class
       
        private List<Student> _subjectStudents; //students who are enrolled in a subject
        
        private bool classroomIsActive = false; //active is when the teacher starts the server

        //instantiate the classroom class after the authentication of the teacher
        public ClassroomManager(Teacher teacher)
        {
            _teacher = teacher;
            //getting the list of the teacher's subjects
            _teachersSubjects = _teacher.AllSubjects;
            //getting the list of the teacher's students and add a subject for it
            Subject allstudentssubject = new Subject(0, "All Students", _teacher.GetID);
            allstudentssubject.RegisteredStudents = _teacher.AllStudents;
            _currentActiveSubject = allstudentssubject;
            _teachersSubjects.Add(allstudentssubject); 
            //_allStudents = _teacher.AllStudents;

            //instanting empty list for students in a subject
            _subjectStudents = new List<Student>();
            //_currentActiveSubject = null;
        }
        //Properties
        public Teacher GetTeacher { get { return _teacher; } }
        public List<Subject> GetSubjects { get { return _teachersSubjects; } }
        public List<Student> GetTeachersStudents { get { return _allStudents; } }
        public List<Student> GetSubjectStudents
        {
            get
            {
                if(_currentActiveSubject != null)
                {
                    return _currentActiveSubject.RegisteredStudents;
                }
                return _allStudents;
            }
        }
        //------------------------active--------------------------//

        public void StartClass(Subject subject)
        {
        //    ServerController.SetupServer();
        }

        // retrieving current subject
        public Subject CurrentSubject
        {
            get { return _currentActiveSubject; }
            set { _currentActiveSubject = value;  }
        }
        public void GetStudentsInASubject(int subject_id)
        {

        }

        public void RegisterUnregisteredStudents()
        {

        }
        //------------------------Inactive/Active--------------------------//

        public bool AddStudent(Student student)
        {
            return _teacher.AddStudent(student);
        }
        public void AddSubject(Subject subject)
        {
            _teacher.AddSubject(subject);
        }       

        public void SubjectStudentRelation()
        {

        }
    }
}