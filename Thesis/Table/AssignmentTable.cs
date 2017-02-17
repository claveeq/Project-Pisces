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
    class AssignmentTable
    {
        [PrimaryKey, AutoIncrement, Column("_Id")]

        public int assignment_id { get; set; } // AutoIncrement and set primarykey  

        [MaxLength(20), NotNull]

        public int assignment_teachersID { get; set; }

        [MaxLength(20), NotNull]

        public string assignment_subject{ get; set; }

        [MaxLength(20), NotNull]

        public string assignment_dateCreated { get; set; }

        [MaxLength(50), NotNull]

        public string assignment_title { get; set; }

        [MaxLength(200), NotNull]

        public string assignment_description { get; set; }
    }
   
}