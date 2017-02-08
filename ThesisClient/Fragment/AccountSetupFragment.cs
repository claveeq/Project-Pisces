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
using Android.Support.Design.Widget;
using ThesisClient.Model;
using ThesisClient.Activities;

namespace ThesisClient.Fragment
{
    public class AccountSetupFragment : Android.App.Fragment
    {
        EditText etPasscode;
        Button btnSetup;
        DashboardActivity dashActivity;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your fragment here
        }
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            etPasscode = View.FindViewById<EditText>(Resource.Id.fragment_accountSetup_etPasscode);
            btnSetup = View.FindViewById<Button>(Resource.Id.fragment_accountSetup_btnSetup);
            btnSetup.Click += BtnSetup_Click;
            dashActivity = Activity as DashboardActivity;
        }
        private void BtnSetup_Click(object sender, EventArgs e)
        {
            if(etPasscode.Text == string.Empty)
            {
                Snackbar.Make(btnSetup, "Your passcode, please:)", Snackbar.LengthShort).Show();
                return;
            }
 
            Settings settings = new Settings();
            settings.Passcode = etPasscode.Text;
       
            BinarySerializer.SerializeSettings(settings);
            dashActivity.ReplaceFragment(dashActivity.homeFragment);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.fragment_accountSetUp, container, false);
            return view;
        }
    }
}