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
namespace Thesis.Fragments
{
    public class HomeFragment : Fragment
    {
        Spinner spinnerSubject;
        Button btnStartClass;

        ClassroomManager classManager;
        DashboardActivity dashActivity;

        List<Subject> subjects;
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

            dashActivity = (DashboardActivity)Activity;
            classManager = dashActivity.GetClassManager;
            subjects = classManager.GetSubjects;

            spinnerSubject.Prompt = "Select Subject...";

            var adapter = new SubjectSpinnerAdapter(Activity, subjects);
            spinnerSubject.Adapter = adapter;

            btnStartClass.Click += BtnStartClass_Click;
            spinnerSubject.ItemSelected += SpinnerSubject_ItemSelected;

        }

        private void SpinnerSubject_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var spinner = sender as Spinner;

            //.MakeText(Activity, subjects[e.Position].GetTeachersID, ToastLength.Long).Show();

        }

        private void BtnStartClass_Click(object sender, EventArgs e)
        {
            ServerController.FireUp();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.fragment_home, container, false);
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            // return inflater.Inflate()
            return view;
        }
    }
}