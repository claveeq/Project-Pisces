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
using System.IO;
using Thesis.Table;

namespace Thesis
{
    public static class DBManager
    {
        //SQLite Configurations
        private static string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "local.db3"); //Calling Database
        private static SQLiteConnection db  = new SQLiteConnection(dpPath);

        public static bool init(Teacher teacher)
        {
            try
            {
                //If doesn't exist, it will recreate the tables  
                db.CreateTable<SubjectsTable>();
                db.CreateTable<TeacherLoginTable>();
                db.CreateTable<SubjectStudentsTable>();
                db.CreateTable<StudentTable>();
                //Creating default subject for all students; 
                var teacherdata = db.Table<TeacherLoginTable>().Where(i => i.username == teacher.GetUsername).FirstOrDefault();
                if(teacherdata == null)
                {
                    SubjectsTable subjectTable = new SubjectsTable();
                    subjectTable.subject_title = "All Students";
                    subjectTable.subject_teachers_id = teacher.GetID;
                    subjectTable.subject_id = 0;
                    db.Insert(subjectTable);
                }
                return true;
            }
            catch(SQLiteException ex)
            {
                return false;
            }
        }
        //Teacher's Data
        public static int GetTeachersID(string username) //get the id first then the rest using the retrieved id
        {
            var teachersdata = db.Table<TeacherLoginTable>().Where(i => i.username == username).FirstOrDefault();
            return teachersdata.id;
        }
        public static string GetTeachersFullname(int id)
        {
            var teachersdata = db.Table<TeacherLoginTable>().Where(i => i.id == id).FirstOrDefault();
            return teachersdata.fullname;
        }
        public static List<Subject> GetTeachersSubjects(int id)
        {
            List<Subject> subjects = new List<Subject>();

            var subjecttbl = db.Table<SubjectsTable>();
            var teachersdata = subjecttbl.Where(i => i.subject_teachers_id == id);
            foreach(var item in teachersdata)
                subjects.Add(new Subject(item.subject_id, item.subject_title, item.subject_teachers_id));
            return subjects;
        }

        public static List<Student> GetTeachersStudents(int id)
        {
            List<Student> students = new List<Student>();
            var studentsTable = db.Table<StudentTable>();
            var studentData = studentsTable.Where(i => i.student_teachers_id == id);
            foreach(var item in studentData)
                students.Add(new Student(item.student_id));
            return students;
        }

        public static void InsertStudent(Student student, int teachersID)
        {
            db.CreateTable<StudentTable>();
            StudentTable studentTable = new StudentTable();
            studentTable.student_firstname = student.GetFirstName;
            studentTable.student_lastname = student.GetLastName;
            studentTable.student_passcode = student.GetPasscode;
            studentTable.student_teachers_id = teachersID;
            db.Insert(studentTable);
            //getting data using passcode
            var data = db.Table<StudentTable>();
            var data1 = data.Where(x => x.student_passcode == student.GetPasscode).FirstOrDefault();
            //creating relationship with student and subject
            db.CreateTable<SubjectStudentsTable>();
            SubjectStudentsTable subjStudentTable = new SubjectStudentsTable();
            subjStudentTable.subj_stud_student_id = data1.student_id;
            subjStudentTable.subj_stud_teachers_id = teachersID;
            subjStudentTable.subj_stud_subject_id = 0;
            db.Insert(subjStudentTable);
        }
        public static void InsertSubject(Subject subject)
        {
            SubjectsTable subjectTable = new SubjectsTable();
            subjectTable.subject_title = subject.GetTitle;
            subjectTable.subject_teachers_id = subject.GetTeachersID;
            db.Insert(subjectTable);
        }
        public static Student GetStudent(string passcode, int teachersID)
        {
            var studentTable= db.Table<StudentTable>();
            var studentdata = studentTable.Where(i => i.student_passcode == passcode).FirstOrDefault();
            return new Student(studentdata.student_id);
        }
        public static Subject GetSubject(string title, int teachersID)
        {
            var subjecttbl = db.Table<SubjectsTable>();
            var subjectdata = subjecttbl.Where(i => i.subject_title == title).FirstOrDefault();
            return new Subject(subjectdata.subject_id, subjectdata.subject_title, teachersID);
        }
        //End teachers Data
        //Student Data
        public static string GetStudentFirstName(int id) //get the id first then the rest using the retrieved id
        {
            var studentsdata = db.Table<StudentTable>().Where(i => i.student_id == id).FirstOrDefault();
            return studentsdata.student_firstname;
        }
        public static string GetStudentLastName(int id)
        {
            var studentsdata = db.Table<StudentTable>().Where(i => i.student_id == id).FirstOrDefault();
            return studentsdata.student_lastname;
        }
        public static int GetStudentTeachersID(int id) 
        {
            var studentsdata = db.Table<StudentTable>().Where(i => i.student_id == id).FirstOrDefault();
            return studentsdata.student_teachers_id;
        }
        public static string GetStudentPasscode(int id)
        {
            var studentsdata = db.Table<StudentTable>().Where(i => i.student_id == id).FirstOrDefault();
            return studentsdata.student_passcode;
        }
        //End Student Data
        //Subject Data
        public static void ToggleStudentInASubject(int teachersID, int subjectID, int studentsID, bool inThisSubject)
        {
            SubjectStudentsTable subjectStudentsTable = new SubjectStudentsTable();
            subjectStudentsTable.subj_stud_teachers_id = teachersID;
            subjectStudentsTable.subj_stud_subject_id = subjectID;
            subjectStudentsTable.subj_stud_student_id = studentsID;
            if(inThisSubject)
            {
                db.Delete(subjectStudentsTable);
            }
            else
            {
                db.Insert(subjectStudentsTable);
            } 
        }
        internal static void DeleteStudent(Student student)
        {
            throw new NotImplementedException();
        }
        internal static void DeleteSubject(Subject subject)
        {
            throw new NotImplementedException();
        }
    }
}