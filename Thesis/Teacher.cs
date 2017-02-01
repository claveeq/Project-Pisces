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
        public Teacher(string username, string password)
        {
            _username = username;
            _password = password;
            _ID = DBManager.GetTeachersID(_username);
            _fullName = DBManager.GetTeachersFullname(_ID);

            _allSubjects = new List<Subject>();
            _allSubjects = DBManager.GetTeachersSubjects(_ID);
            _allStudents = new List<Student>();
            _allStudents = DBManager.GetTeachersStudents(_ID);
        }
        public void AddSubject(Subject subject)
        {// Adding subject to db and subject list 
            DBManager.InsertSubject(subject);
            _allSubjects.Add(DBManager.GetSubject(subject.GetTitle, _ID));
        }

        public void DeleteSubject(Subject subject)
        {// Deleting subject from db and subject list 

            DBManager.DeleteSubject(subject);
            _allSubjects.Remove(subject);
            //_allSubjects.Remove(DBManager.GetSubject(subject.GetTitle, _ID));
        }
        public void AddStudent(Student student)
        {// Adding subject to db 
            DBManager.InsertStudent(student, _ID); 
        }

        public void DeleteStudent(Student student)
        {// Deleting subject from db 
            
            DBManager.DeleteStudent(student);
        }

   
    }
}