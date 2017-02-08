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
    public class AccountFragment : Android.App.Fragment
    {
        EditText etPasscode;
        Button btnSetup;
        DashboardActivity dashActivity;
        StudentManager studentManager;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        private void initViews()
        {
            // Create your fragment here
            etPasscode = View.FindViewById<EditText>(Resource.Id.fragment_account_etPasscode);
            btnSetup = View.FindViewById<Button>(Resource.Id.fragment_account_btnSave);
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            initViews();

            dashActivity = Activity as DashboardActivity;
            //      studentManager = dashActivity.sub
            etPasscode.Text = dashActivity.settings.Passcode;
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.fragment_account, container, false);
            return view;
        }
    }
}