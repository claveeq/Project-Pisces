using Android.Content;
using Android.Widget;
using SQLite;
using System;
using System.IO;
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

//        public static bool AuthStudent(Student student, Context context)
//        {
//            try
//            {
//                string dpPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "local.db3"); //Call Database  
//                var db = new SQLiteConnection(dpPath);
//                var data = db.Table<StudentTable>(); //Call Table  
///*                var data1 = data.Where(x => x.student_passcode == txtusername.Text && x.password == txtPassword.Text).FirstOrDefault(); *///Linq Query  
//                var data1 = data.Where(x => x.student_passcode == student && x.student_macAddress == student.GetMacAddress).FirstOrDefault();
//                if(data1 != null)
//                {
//                    return true;
//                }
//                else
//                {
//                    return false;
//                }
//            }
//            catch(Exception ex)
//            {
//                Toast.MakeText(context, ex.ToString(), ToastLength.Short).Show();
//                return false;
//            }
//        }

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
            catch(Exception)
            {
                return false;
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

        //public static bool CreateStudent(Student student, Context context)
        //{
        //    try
        //    {
        //        string dpPath = Path.Combine(Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "local.db3");
        //        var db = new SQLiteConnection(dpPath);
        //        db.CreateTable<StudentTable>();
        //        StudentTable tbl = new StudentTable();
        //        tbl.student_name = student.GetName;
        //        tbl.student_passcode = student.GetPasscode;
        //        tbl.student_macAddress = student.GetMacAddress;
        //        db.Insert(tbl);
        //        return true;
        //    }
        //    catch(Exception ex)
        //    {
        //        Toast.MakeText(context, ex.ToString(), ToastLength.Short).Show();
        //        return false;
        //    }
        //}
    }
}
