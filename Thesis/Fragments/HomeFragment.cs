using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.App;
using Thesis.Activities;
using Thesis.Adapter;
using Android.Support.Design.Widget;
using System.Timers;
using System.IO;

namespace Thesis.Fragments
{
    public class HomeFragment : Fragment
    {
        Spinner spinnerSubject;
        Button btnStartClass;
        Button btnRefreshIP;
        Button btnLogout;
        TextView ipaddress;
        Button btnOpenAttendanceFolder;
        ClassroomManager classManager;
        DashboardActivity dashActivity;
        
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here

        }
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            spinnerSubject = View.FindViewById<Spinner>(Resource.Id.spinnerSubjectsHome);
            btnStartClass = View.FindViewById<Button>(Resource.Id.buttonStartClass);
            ipaddress = View.FindViewById<TextView>(Resource.Id.fragment_home_ipaddress);
            btnRefreshIP = View.FindViewById<Button>(Resource.Id.fragment_home_btRefresIP);
            btnOpenAttendanceFolder = View.FindViewById<Button>(Resource.Id.fragment_btnOpenAttendanceDirectory);
            btnLogout = View.FindViewById<Button>(Resource.Id.fragment_btnLogout);
            dashActivity = (DashboardActivity)Activity;
            classManager = dashActivity.GetClassManager;

            spinnerSubject.Prompt = "Select Subject...";

            var adapter = new SubjectSpinnerAdapter(Activity, classManager.GetSubjects);
            spinnerSubject.Adapter = adapter;
            btnRefreshIP.Click += delegate
            {
                ipaddress.Text = ServerController.GetIPAddress(Activity);
            };
            btnStartClass.Click += BtnStartClass_Click;
            spinnerSubject.ItemSelected += SpinnerSubject_ItemSelected;
            btnOpenAttendanceFolder.Click += delegate
             {
                 string folderlocation;
                 if(Android.OS.Environment.ExternalStorageState.Equals(Android.OS.Environment.MediaMounted))
                     folderlocation = Android.OS.Environment.ExternalStorageDirectory.Path;
                 else
                     folderlocation = Android.OS.Environment.DirectoryDocuments;

                 folderlocation += @"/PICAttendance";
                 if(!Directory.Exists(folderlocation))
                     Directory.CreateDirectory(folderlocation);

                 Intent intent = new Intent(Intent.ActionView);
                 var uri = Android.Net.Uri.Parse(folderlocation);
                 intent.SetDataAndType(uri, "resource/folder");
                 StartActivity(Intent.CreateChooser(intent, "Attendance Folder"));
             };
            btnLogout.Click += delegate
            {
                var builder = new Android.Support.V7.App.AlertDialog.Builder(dashActivity);
                builder.SetTitle("Confirm Logout!");
                builder.SetMessage("Are you sure you want to logout?");
                builder.SetPositiveButton("Yes", (senderAlert, args) => {

                    Activity.StartActivity(typeof(MainActivity));
                    Activity.Finish();
                });
                builder.SetNegativeButton("Cancel", (senderAlert, args) => { });

                Dialog dialog = builder.Create();
                dialog.Show();
    
            };
            ipaddress.Text = ServerController.GetIPAddress(Activity);
        }

        private void SpinnerSubject_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            classManager.CurrentSubject = classManager.GetSubjects[e.Position];
        }

        private void BtnStartClass_Click(object sender, EventArgs e)
        {
            if(classManager.StartClass(ipaddress.Text, 5))//TO Be Added
            {
                classManager.ClassroomIsActive = true;
                dashActivity.ReplaceFragment(dashActivity.activeHomeFragment);
            }
            else
            {
                Snackbar.Make(btnStartClass, "There's a Problem with your Connection.",Snackbar.LengthShort).Show();
            }     

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragme
            View view = inflater.Inflate(Resource.Layout.fragment_home, container, false);
            return view;
        }
    }
}