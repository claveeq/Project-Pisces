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

namespace Thesis.Table
{
    class SubjectStudentsTable
    {
        [PrimaryKey, AutoIncrement, Column("_Id")]

        public int subj_stud_id { get; set; }

        [MaxLength(25)]

        public int subj_stud_teachers_id { get; set; }

        [MaxLength(25)]

        public int subj_stud_subject_id { get; set; }

        [MaxLength(25)]

        public int subj_stud_student_id { get; set; }

    }
}