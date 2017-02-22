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

namespace ThesisClient.Model
{
    [Serializable]
    public class Assignment
    {
        private string title;
        private DateTime dateCreated;
      //  private DateTime deadline;
        private bool isDone = false;
        private string subject;
        private string description;
        public Assignment(string title, string description, DateTime datecreated, string subject)
        {
            this.title = title;
            dateCreated = datecreated;
          //  this.deadline = deadline;
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
        //public string Deadline
        //{
        //    get { return deadline.ToString(); }
        //}
    }
}