using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using ThesisClient.Adapter;
using System.IO;
using ThesisClient.Activities;

namespace ThesisClient.Fragment
{
    public class LectureFragment : Android.App.Fragment
    {
        LecturesAdapter lecturesAdapter;
        ListView lvLectures;
        Button btnOpenLectureFolder;
        DashboardActivity dashActivity;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            dashActivity = Activity as DashboardActivity;
            btnOpenLectureFolder = View.FindViewById<Button>(Resource.Id.fragment_lecture_btnOpenLectureFolder);
            lvLectures = View.FindViewById<ListView>(Resource.Id.fragment_lecture_lvLectures);
            lecturesAdapter = new LecturesAdapter(Activity,dashActivity.studentManager.GetLecturesFileNames());
            lvLectures.Adapter = lecturesAdapter;

            btnOpenLectureFolder.Click += delegate
            {
                string folderlocation;
                if(Android.OS.Environment.ExternalStorageState.Equals(Android.OS.Environment.MediaMounted))
                    folderlocation = Android.OS.Environment.ExternalStorageDirectory.Path;
                else
                    folderlocation = Android.OS.Environment.DirectoryDocuments;

                folderlocation += @"/Lectures";
                if(!Directory.Exists(folderlocation))
                    Directory.CreateDirectory(folderlocation);

                Intent intent = new Intent(Intent.ActionView);
                var uri = Android.Net.Uri.Parse(folderlocation);
                intent.SetDataAndType(uri, "resource/folder");
                StartActivity(Intent.CreateChooser(intent, "Lecture Folder"));
            };
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.fragment_lecture, container, false);
            return view;
        }  
    }
}