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

namespace Thesis.Fragments
{
    public class HomeFragment : Fragment
    {
        Spinner spinnerSubject;
        Button btnStartClass;
        TextView ipaddress;
        ClassroomManager classManager;
        DashboardActivity dashActivity;
        System.Timers.Timer timer;
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

            dashActivity = (DashboardActivity)Activity;
            classManager = dashActivity.GetClassManager;
 
 
            spinnerSubject.Prompt = "Select Subject...";

            var adapter = new SubjectSpinnerAdapter(Activity, classManager.GetSubjects);
            spinnerSubject.Adapter = adapter;

            btnStartClass.Click += BtnStartClass_Click;
            spinnerSubject.ItemSelected += SpinnerSubject_ItemSelected;

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