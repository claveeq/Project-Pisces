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
    public class Settings
    {
        string passcode;
        List<Assignment> _assignment;

        public string Passcode {
            get { return passcode; }
            set { passcode = value; }
        }

        public List<Assignment> propAssignment
        {
            get { return _assignment; }
            set { _assignment = value; }
        }
    }
}