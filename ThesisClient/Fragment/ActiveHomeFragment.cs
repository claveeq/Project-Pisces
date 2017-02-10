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
        Button btnScan;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your fragment here
        }
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            btnScan = View.FindViewById<Button>(Resource.Id.fragment_home_active_btnScan);

            btnScan.Click += BtnScan_Click;
        }

        private void BtnScan_Click(object sender, EventArgs e)
        {
            ClientController.SendRequest(Task.quiz);
            if(ClientController.QuizIsAvailable)
            {
                Intent intent = new Intent(Activity, typeof(QuizActivity));
                intent.PutExtra("passcode", "dfsd");
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