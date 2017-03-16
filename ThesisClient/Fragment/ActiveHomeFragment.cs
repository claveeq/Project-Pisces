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
using ThesisClient.Activities;

namespace ThesisClient.Fragment
{
    public class ActiveHomeFragment : Android.App.Fragment
    {
        DashboardActivity dashActivity;
        Button btnScan;
        Button btnLogout;
        Button btnAssignment;
        Button btnLecture;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your fragment here
        }
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            dashActivity = Activity as DashboardActivity;
            btnScan = View.FindViewById<Button>(Resource.Id.fragment_home_active_btnScan);
            btnLogout = View.FindViewById<Button>(Resource.Id.fragment_home_active_btnLogout);
            btnAssignment = View.FindViewById<Button>(Resource.Id.fragment_home_active_btnAssignment);
            btnLecture = View.FindViewById<Button>(Resource.Id.fragment_home_active_btnLectures);
            btnScan.Click += BtnScan_Click;
            btnLogout.Click += BtnLogout_Click;
            btnAssignment.Click += BtnAssignment_Click;
            btnLecture.Click += BtnLecture_Click;
        }

        private void BtnLecture_Click(object sender, EventArgs e)
        {
            ClientController.SendRequest(Task.lecture);
            // like assignment
        }

        private void BtnAssignment_Click(object sender, EventArgs e)
        {
            ClientController.SendRequest(Task.assignment);
            dashActivity.studentManager.SaveAssignment(ClientController.assignments);
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            var builder = new Android.Support.V7.App.AlertDialog.Builder(Activity);
            builder.SetTitle("Wait!");
            builder.SetMessage("Are you sure you want to logout?");
            builder.SetPositiveButton("Yes", (senderAlert, args) => {
                ClientController.Exit();
                dashActivity.ReplaceFragment(dashActivity.homeFragment);
            });
            builder.SetNegativeButton("Cancel", (senderAlert, args) => { });

            Dialog dialog = builder.Create();
            dialog.Show();
        }

        private void BtnScan_Click(object sender, EventArgs e)
        {
            ClientController.SendRequest(Task.quiz);
            if(ClientController.QuizIsAvailable)
            {
                Intent intent = new Intent(Activity, typeof(QuizActivity));
                intent.PutExtra("passcode", dashActivity.authStudent.GetPasscode);
                StartActivity(intent);
            }
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.fragment_home_active, container, false);
            return view;
        }
    }
}