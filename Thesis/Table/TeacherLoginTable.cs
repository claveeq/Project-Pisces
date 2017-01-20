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
    class TeacherLoginTable
    {
        [PrimaryKey, AutoIncrement, Column("_Id")]

        public int id { get; set; } // AutoIncrement and set primarykey  

        [MaxLength(40)]

        public string username { get; set; }

        [MaxLength(40)]

        public string password { get; set; }

        [MaxLength(40)]

        public string fullname { get; set; }

        //[MaxLength(15)]

        //public DateTime dateCreated { get; set; }
    }
}