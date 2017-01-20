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
    class SubjectsTable
    {
        [PrimaryKey, AutoIncrement, Column("_Id")]
        public int subject_id { get; set; }

        [MaxLength(10)]
        public int subject_teachers_id { get; set; }

        [MaxLength(25)]
        public string subject_title { get; set; }

    }
}