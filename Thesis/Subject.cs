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
    class Subject
    {
        private string _title;
        private string _teacher;
        private TimeSpan _timeLength;
        private List<Student> _students;

        private Quiz _quiz;

        private DateTime _dateToday;
        private DateTime _dateCreated;

        private List<Assignment> _assignments;
        private List<Lecture> _lectures;

        public Subject(string title, string teacher, TimeSpan timeLength, List<Student> students)
        {
            _title = title;
            _teacher = teacher;
            _timeLength = timeLength;
            _students = students;
          //  _dateToday = DateTime.Now();
        }

        public Subject()
        {
        }

        public List<Student> GetRegisteredStudents{ get; set; }
        public int MyProperty { get; set; }
    }
}