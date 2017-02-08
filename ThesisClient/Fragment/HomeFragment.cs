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
using ThesisClient;
using ThesisClient.Activities;
using Android.Support.Design.Widget;

namespace ThesisClient.Fragment
{
    public class HomeFragment : Android.App.Fragment
    {
        EditText etTeachersIPAddress;
        EditText etPasscode;
        Button btnJoinClass;

        DashboardActivity dashActivity;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            initViews();
            dashActivity = (DashboardActivity)Activity;
            btnJoinClass.Click += BtnJoinClass_Click;
        }

        private void BtnJoinClass_Click(object sender, EventArgs e)
        {
            if(ClientController.ConnectToServer(etTeachersIPAddress.Text))
            {
                btnJoinClass.Enabled = false;
                ClientController.context = dashActivity;
                ClientController.Student = new AuthStudent("Clave", etPasscode.Text);
                ClientController.SendRequest(Task.login);
                dashActivity.studentManager.Status = appStatus.active;
                dashActivity.ReplaceFragment(dashActivity.activeHomeFragment);
            }
            else
            {
                Snackbar.Make(btnJoinClass, "Invalid IP Address", Snackbar.LengthShort).Show();
            }
        }

        private void initViews()
        {
            etTeachersIPAddress = View.FindViewById<EditText>(Resource.Id.fragment_home_teahchersIPAddress);
            etPasscode = View.FindViewById<EditText>(Resource.Id.fragment_home_passcode);
            btnJoinClass = View.FindViewById<Button>(Resource.Id.fragment_home_btn_joinClass);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.fragment_home, container, false);
            return view;
        }
    }
}