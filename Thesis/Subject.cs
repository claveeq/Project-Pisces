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

namespace Thesis
{
    public class Subject
    {
        private int _ID;
        private int _teacher_ID;
        private string _title;

        //private TimeSpan _timeLength;
        //private List<Student> _students;

        //private Quiz _quiz;

        //private DateTime _dateToday;
        //private DateTime _dateCreated;

        //private List<Assignment> _assignments;
        //private List<Lecture> _lectures;
        //for Reading subjects
        public Subject(int id, string title, int teachers_id )
        {
            _ID = id;
            _title = title;
            _teacher_ID = teachers_id;
        }
        //for adding subject
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

        public List<Student> GetRegisteredStudents{ get; set; }
        public int MyProperty { get; set; }

        //return title of the object
        public override string ToString()
        {
            return _title; 
        }
    }
}