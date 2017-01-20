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
        int _ID = 1;
        string _username;
        string _password;
        string _fullName;

        List<Subject> _allSubjects;
        List<Student> _allStudents;

        public string GetUsername { get{ return _username; } }
        public string GetPassword { get { return _password; } }
        public string GetFullName { get { return _fullName; } }

        public Teacher()
        {

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

        public Teacher(string name, string password)//forAuth
        {
            _username = name;
            _password = password;
            _allSubjects = new List<Subject>();
            retrieveUserDataFromDB();
        }
        public Teacher(string name, string password, string fullname)
        {
            _username = name;
            _password = password;
            _fullName = fullname;
        }

        public int GetID { get { return _ID; } }
        public List<Subject> AllSubjects
        {
            get { return _allSubjects; }
            private set { _allSubjects = value; }
        }


        public List<Student> AllStudents //returns all students from teachers record
        {
            get { return _allStudents; }
            private set { _allStudents = value; }
        }
        public void retrieveUserDataFromDB()
        {
            string dpPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "local.db3"); //Call Database  
            var db = new SQLiteConnection(dpPath);
            //Teacher's Data in the DB
            var userdata = db.Table<TeacherLoginTable>().Where(i => i.username == _username).FirstOrDefault();
            _fullName = userdata.fullname;
            _ID = userdata.id;
            //Teacher's Subjects in the DB
            var tbl = db.Table<SubjectsTable>(); 
            var subjectdata = tbl.Where(i => i.subject_teachers_id == _ID);
            foreach(var item in subjectdata)
            {
                _allSubjects.Add(new Subject(item.subject_id, item.subject_title, item.subject_teachers_id));
            }
        }

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

        private void SerializeAllStudentsToXml()
        {

        }

        private void DeserializeAllStudentsFromXml()
        {

        }
    }
}