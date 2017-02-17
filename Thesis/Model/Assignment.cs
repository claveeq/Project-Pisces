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

namespace Thesis.Model
{
    public class Assignment
    {
        private string title;
        private DateTime dateCreated;
        private bool isDone = false;
        private string subject;
        private string description;
        public Assignment(string title, string description, DateTime datecreated, string subject)
        {
            this.title = title;
            dateCreated = datecreated;
            this.subject = subject;
            this.description = description;
        }
        public int ID { get; set; }
        public string Title {
            get { return title; }
        }
        public string DateCreated {
            get { return dateCreated.ToString(); }
        }
        public string Description {
            get { return description; }
        }
        public string Subjct {
            get { return subject; }
        }

    }
}