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
using SQLite;
namespace Thesis
{
    public static class DBManager
    {
        //SQLite Configurations
        private static string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "local.db3"); //Calling Database
        private static SQLiteConnection db = new SQLiteConnection(dpPath);

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
            var studentTable = db.Table<StudentTable>();
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
        public static void InsertStudentAttendance(Student student, string date, string time)
        {    
            AttendanceTable attendance = new AttendanceTable();
            attendance.attendace_student_id = student.GetID;
            attendance.attendace_subjects_id = student.CurrentSubjectID;
            attendance.attendace_teachers_id = student.Teachers_ID;
            attendance.attendace_date = date;
            attendance.attendace_time = time;
            db.Insert(attendance);
        }   
        //End Student Data
        //Subject Data
        public static bool ToggleStudentInASubject(Student student)
        {
         
            var studentsubjectdata = db.Table<SubjectStudentsTable>()
                           .Where(i => i.subj_stud_teachers_id == student.Teachers_ID &&
                           i.subj_stud_subject_id == student.CurrentSubjectID &&
                           i.subj_stud_student_id == student.GetID).FirstOrDefault();
            if(studentsubjectdata == null)
            {
                SubjectStudentsTable subjectStudentsTable = new SubjectStudentsTable();
                subjectStudentsTable.subj_stud_teachers_id = student.Teachers_ID;
                subjectStudentsTable.subj_stud_subject_id = student.CurrentSubjectID;
                subjectStudentsTable.subj_stud_student_id = student.GetID;
                db.Insert(subjectStudentsTable);
                return true;
            }
            else
            {
                db.Delete(studentsubjectdata);
                return false;
            } 
        }
        public static void DeleteStudent(Student student)
        {
            var allstudent = db.Table<SubjectStudentsTable>()
                .Where(i => i.subj_stud_student_id == student.GetID && 
                i.subj_stud_teachers_id == student.Teachers_ID);
            foreach(var item in allstudent)
            {
                db.Delete(item);
            }
            var studentsdata = db.Table<StudentTable>().Where(i => i.student_id == student.GetID).FirstOrDefault();
            db.Delete(studentsdata);

        }
        public static void DeleteSubject(Subject subject)
        {
            var allsubjects = db.Table<SubjectStudentsTable>()
           .Where(i => i.subj_stud_teachers_id == subject.GetTeachersID &&
           i.subj_stud_subject_id == subject.ID);
            foreach(var item in allsubjects)
            {
                db.Delete(item);
            }
            var subject_id = db.Table<SubjectsTable>()
                .Where(i => i.subject_id == subject.ID).FirstOrDefault();
            db.Delete(subject_id);

        }

        //other utilities
        public static int CountStudentTable()
        {
            var students= db.Table<StudentTable>();
            return students.Count();
        }
        public static int CountSubjectTable()
        {
            var subjects = db.Table<SubjectsTable>();
            return subjects.Count();
        }
        public static int CountSubjectStudentTable()
        {
            var subjectstudents = db.Table<SubjectStudentsTable>();
            return subjectstudents.Count();
        }
    }
}