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
using System.IO;

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

        public void SaveAssignment(List<Assignment> assignments)
        {
            settings.propAssignment = assignments;
            BinarySerializer.SerializeSettings(settings);
        }

        public List<string> GetLecturesFileNames()
        {
            string folderlocation;
            if(Android.OS.Environment.ExternalStorageState.Equals(Android.OS.Environment.MediaMounted))
                folderlocation = Android.OS.Environment.ExternalStorageDirectory.Path;
            else
                folderlocation = Android.OS.Environment.DirectoryDocuments;

            folderlocation += @"/Lectures/";
            if(!Directory.Exists(folderlocation))
                Directory.CreateDirectory(folderlocation);

            string filepath = folderlocation;
            string[] files = Directory.GetFiles(folderlocation);
            var filenames = new List<string>();
            if(files != null)
            {
                foreach(var file in files)
                {
                    FileInfo fi = new FileInfo(file);
                    //  fi.Name.Replace(fi.Extension, "");
                    filenames.Add(fi.Name);
                }
            }
            return filenames;
        }
    }
}