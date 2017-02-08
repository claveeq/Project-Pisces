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
using ThesisClient.Model;

namespace ThesisClient
{
    public enum appStatus { active, inactive}
    public class StudentManager
    {
        appStatus _status;
        Settings settings;
        
        public StudentManager(Settings settings)
        {
            this.settings = settings;
            _status = appStatus.inactive;
        }

        public appStatus Status
        {
            get { return _status; }
            set { _status = value; }
        }
    }
}