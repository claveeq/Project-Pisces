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

        //private TimeSpan _timeLength;
        private List<Student> _registeredStudents;

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
            _registeredStudents = new List<Student>();
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
        public int GetID { get { return _ID; }  }
        public string GetTitle { get { return _title; } }
        public int GetTeachersID { get { return _teacher_ID;  } }
        public List<Student> RegisteredStudents
        {
            get { return _registeredStudents; }
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
            var subjectStudentData = subjectStudenttable.Where(i => i.subj_stud_teachers_id == _teacher_ID);
            foreach(var item in subjectStudentData)
            {
                //if the student is registered in the subject
                var data = _registeredStudents.Where(x => x.GetID == item.subj_stud_student_id).FirstOrDefault();
                if(data == null)
                {
                    if(item.subj_stud_student_id == _ID)
                    {
                        Student student = new Student(item.subj_stud_student_id);
                        student.inThisSubjects = true;
                        _registeredStudents.Add(student);
                    }
                    else
                    {
                        _registeredStudents.Add(new Student(item.subj_stud_student_id));
                    }
                }            
            }
            
            //_subjectStudents.Clear();

            //foreach(var item in _allStudents)
            //{
            //    item.CurrentSubjectID = 0;
            //}
            //foreach(var subjectStudent in subjectStudentData)
            //{
            //    foreach(var student in _allStudents)
            //    {
            //        if(subjectStudent.subj_stud_student_id == student.GetID)
            //        {
            //            student.CurrentSubjectID = subjectID;
            //            _subjectStudents.Add(student);
            //        }
            //        _subjectStudents.Add(student);
            //    }
            //}
        //}
        }

        //return title of the object
        public override string ToString()
        {
            return _title; 
        }
    }
}