using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using Thesis.Table;

namespace Thesis
{
    public class Subject
    {
        private int _ID;
        private int _teacher_ID;
        private string _title;

        private TimeSpan _timeLength;
        private TimeSpan _lateIn;
        private List<Student> _registeredStudents = new List<Student>();

        //private Quiz _quiz;

        //private DateTime _dateToday;
        //private DateTime _dateCreated;

        //private List<Assignment> _assignments;
        //private List<Lecture> _lectures;

        //for Instantiating a subject
        public Subject(int id, string title, int teachers_id )
        {
            _ID = id;
            _title = title;
            _teacher_ID = teachers_id;
        }
        //for creating a subject
        public Subject(string title, int teachers_id)
        {
            _title = title;
            _teacher_ID = teachers_id;

        }

        //public Subject(string title, string teacher, TimeSpan timeLength, List<Student> students)
        //{
        //    _title = title;
        //    _teacher = teacher;
        //    _timeLength = timeLength;
        //    _students = students;
        //  //  _dateToday = DateTime.Now();
        //}
        public int ID {
            get { return _ID; }
            set { _ID = value; }
        }
        public string GetTitle { get { return _title; } }
        public int GetTeachersID { get { return _teacher_ID;  } }
        public List<Student> RegisteredStudents
        {
            get
            {
             //   _registeredStudents.Clear();
                retrieveStudentsfromDB();
                return _registeredStudents;
            }
            set { _registeredStudents = value; }
        }
        public int MyProperty { get; set; }

        public void retrieveStudentsfromDB()
        {
           // _registeredStudents.Clear();
            string dpPath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "local.db3"); //Call Database  
            var db = new SQLiteConnection(dpPath);
            //All Teacher's Students from the DB
            var subjectStudenttable = db.Table<SubjectStudentsTable>();
            var allstudents = subjectStudenttable.Where(i => i.subj_stud_teachers_id == _teacher_ID);
            var registeredstudents = allstudents.Where(i => i.subj_stud_subject_id == _ID);
            var unregisteredstudents = allstudents.Where(i => i.subj_stud_subject_id != _ID);

            foreach(var item in registeredstudents)
            {
                Student student = new Student(item.subj_stud_student_id);
                student.inThisSubjects = true;
                student.CurrentSubjectID = _ID;
                var studentexist = _registeredStudents.Where(x => x.GetID == item.subj_stud_student_id).FirstOrDefault();
                if(studentexist == null)
                {
                    _registeredStudents.Add(student);
                }
            }
            foreach(var item in unregisteredstudents)
            {
                Student student = new Student(item.subj_stud_student_id);
                student.inThisSubjects = false;
                student.CurrentSubjectID = _ID;
                var studentexist = _registeredStudents.Where(x => x.GetID == item.subj_stud_student_id).FirstOrDefault();
                //   if(!_registeredStudents.Contains(student))
                if(studentexist == null)
                {
                    _registeredStudents.Add(student);
                }
            }
        }

        //return title of the object
        public override string ToString()
        {
            return _title; 
        }
    }
}