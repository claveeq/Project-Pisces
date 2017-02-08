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
    class AttendanceTable
    {
        [PrimaryKey, AutoIncrement, Column("_Id")]

        public int attendace_id { get; set; } // AutoIncrement and set primarykey  

        [MaxLength(25), NotNull]

        public int attendace_student_id { get; set; }

        [MaxLength(25), NotNull]

        public int attendace_teachers_id { get; set; }

        [MaxLength(25), NotNull]

        public int attendace_subjects_id { get; set; }

        [MaxLength(25), NotNull]

        public string attendace_date { get; set; }

        [MaxLength(25)]

        public string attendace_time { get; set; }
    }
}