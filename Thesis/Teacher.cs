using System.Collections.Generic;
using System.Data;
using System.Linq;
using Java.Lang;
using System;
using Thesis.Table;
using SQLite;
using System.IO;

namespace Thesis
{
    public class Teacher
    {
        int _ID;
        string _username;
        string _password;
        string _fullName;
        List<Subject> _allSubjects;
        List<Student> _allStudents;

        public string GetUsername { get{ return _username; } }
        public string GetFullName { get { return _fullName; } }
        public int GetID { get { return _ID; } }

        //returns all subjects from this instance
        public List<Subject> AllSubjects {
            get { return _allSubjects; }
            private set { _allSubjects = value; }
        }
        //returns all students from this instance
        public List<Student> AllStudents 
        {
            get { return _allStudents; }
            private set { _allStudents = value; }
        }
        //For instantiation and retrieval of user's data from DB
        public Teacher(string name, string password)
        {
            _username = name;
            _password = password;
            _allSubjects = new List<Subject>();
            _allStudents = new List<Student>();
            retrieveUserDataFromDB();
        }
        //retrieval of user's data from DB
        public void retrieveUserDataFromDB()
        {
            string dpPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "local.db3"); //Call Database  
            var db = new SQLiteConnection(dpPath);
            //Teacher's Data from the DB
            var teachersdata = db.Table<TeacherLoginTable>().Where(i => i.username == _username).FirstOrDefault();
            _fullName = teachersdata.fullname;
            _ID = teachersdata.id;
            //Teacher's Subjects from the DB
            var subjecttbl = db.Table<SubjectsTable>(); 
            var subjectdata = subjecttbl.Where(i => i.subject_teachers_id == _ID);
            foreach(var item in subjectdata)
                _allSubjects.Add(new Subject(item.subject_id, item.subject_title, item.subject_teachers_id));
            //All Teacher's Students from the DB
            var studentsTable = db.Table<StudentTable>();
            var studentData = studentsTable.Where(i => i.student_teachers_id == _ID);
            foreach(var item in studentData)
                _allStudents.Add(new Student(item.student_id));
        }
        // Adding student to db and student list 
        public bool AddStudent(Student student)
        {
            student.Teachers_ID = _ID;
            if(Auth.CreateStudent(student, this))
            {
                _allStudents.Add(student);
                return true;
            }
            return false;
        }
        // Adding subject to db and subject list 
        public void AddSubject(Subject subject)
        {
            if(!_allSubjects.Contains(subject))
            {
                string dpPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "local.db3");
                var db = new SQLiteConnection(dpPath);
                db.CreateTable<SubjectsTable>();
                SubjectsTable tbl = new SubjectsTable();
                tbl.subject_title = subject.GetTitle;
                tbl.subject_teachers_id = subject.GetTeachersID;
                db.Insert(tbl);
                _allSubjects.Add(subject);
            }
        }

        public void updateFullnameDb(string fullname)
        {
            string dpPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "local.db3"); //Call Database  
            var db = new SQLiteConnection(dpPath);
            var item = db.Get<TeacherLoginTable>(_username);
            item.fullname = fullname;
            db.Update(item);
            db.Close();
        }

        private void SerializeAllStudentsToXml()
        {

        }

        private void DeserializeAllStudentsFromXml()
        {

        }
    }
}