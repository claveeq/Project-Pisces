using Android.App;
using Android.Content;
using Android.Net.Wifi;
using Android.Widget;
using SQLite;
using System;
using System.IO;
using System.Runtime.Remoting.Contexts;
using Thesis.Table;

namespace Thesis
{
    static class Auth
    {
        //Initialize local database in android
        public static string Init()
        {
            var output = "";
            output += "Creating Databse if it doesnt exists";
            string dpPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "local.db3"); //Create New Database  
            var db = new SQLiteConnection(dpPath);
            db.CreateTable<SubjectsTable>();
            db.CreateTable<TeacherLoginTable>();
            db.CreateTable<SubjectStudentsTable>();
            db.CreateTable<StudentTable>();
            output += "\n Database Created....";
            return output;
        }

        public static bool AuthStudent(AuthStudent student)
        {
            try
            {
                string dpPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "local.db3"); //Call Database  
                var db = new SQLiteConnection(dpPath);
                var data = db.Table<StudentTable>(); //Call Table  
                var data1 = data.Where(x => x.student_passcode == student.GetPasscode).FirstOrDefault();
                // && x.student_macAddress == student.GetMacAddress
                if(data1 != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception)
            {
                //Toast.MakeText(context, ex.ToString(), ToastLength.Short).Show();
                return false;
            }
        }
        public static bool StudentExist(string passcode)
        {
            try
            {
                string dpPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "local.db3"); //Call Database  
                var db = new SQLiteConnection(dpPath);
                var tbl = db.Table<StudentTable>(); //Call Table                           /*                var data1 = data.Where(x => x.student_passcode == txtusername.Text && x.password == txtPassword.Text).FirstOrDefault(); *///Linq Query  
                var data1 = tbl.Where( x => x.student_passcode == passcode).FirstOrDefault();
                //returns true if the account dosn't exist
                if(data1 == null)
                {
                    return false;
                } 
                else
                {
                    return true;
                }
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        //public static bool CreateStudent(Student student, Teacher teacher)
        //{
        //    //try
        //    //{
        //    string dpPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "local.db3");
        //    var db = new SQLiteConnection(dpPath);
        //    db.CreateTable<StudentTable>();
        //    StudentTable studentTable = new StudentTable();
        //    studentTable.student_firstname = student.GetFirstName;
        //    studentTable.student_lastname= student.GetLastName;
        //    studentTable.student_passcode = student.GetPasscode;
        //    studentTable.student_teachers_id = student.Teachers_ID;
        //    db.Insert(studentTable);

        //    var data = db.Table<StudentTable>(); 
        //    var data1 = data.Where(x => x.student_passcode == student.GetPasscode).FirstOrDefault();
     
        //    db.CreateTable<SubjectStudentsTable>();
        //    SubjectStudentsTable subjStudentTable = new SubjectStudentsTable();
        //    subjStudentTable.subj_stud_student_id = data1.student_id;
        //    subjStudentTable.subj_stud_teachers_id = teacher.GetID;
        //    db.Insert(subjStudentTable);
        //    return true;
        //    //}
        //    //catch(SQLiteException ex)
        //    //{
        //    //    Toast.
        //    //    return false;
        //    //}
        //}
        public static bool AuthTeacher(string username, string password)
        {
            try
            {
                string dpPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "local.db3"); //Call Database  
                var db = new SQLiteConnection(dpPath);
                var data = db.Table<TeacherLoginTable>(); //Call Table  
                //var data1 = data.Where(x => x.student_passcode == txtusername.Text && x.password == txtPassword.Text).FirstOrDefault(); ////Linq Query  
                var data1 = data.Where(x => x.username == username && x.password == password).FirstOrDefault();
                if(data1 != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        public static bool SubjectExist(string title)
        {
            string dpPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "local.db3"); //Call Database  
            var db = new SQLiteConnection(dpPath);
            var tbl = db.Table<SubjectsTable>(); //Call Table                  
            var data1 = tbl.Where(x => x.subject_title == title).FirstOrDefault();
            //returns true if the account dosn't exist
            if(data1 == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static bool CreateTeacher(string username, string password, string fullname)
        {
            try
            {
                string dpPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "local.db3");
                var db = new SQLiteConnection(dpPath);
                db.CreateTable<TeacherLoginTable>();
                TeacherLoginTable tbl = new TeacherLoginTable();
                tbl.username = username;
                tbl.password = password;
                tbl.fullname = fullname;          
                db.Insert(tbl);
                return true;
            }
            catch(Exception ex)
            {       
                return false;
            }
        } 
    }
}
