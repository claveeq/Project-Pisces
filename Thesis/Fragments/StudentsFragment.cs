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
using Thesis.Activities;
using Thesis.Adapter;
namespace Thesis.Fragments
{
    public class StudentsFragment : Fragment 
    {
        Spinner spinner;
        ClassroomManager classManager;
        DashboardActivity dashActivity;
        List<Subject> subject;
        SubjectSpinnerAdapter adapter;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            dashActivity = (DashboardActivity)Activity;
            classManager = dashActivity.GetClassManager;

            subject = classManager.GetSubjects;
            base.OnActivityCreated(savedInstanceState);
            spinner = View.FindViewById<Spinner>(Resource.Id.spinner1);
          //  spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            spinner.Prompt = "Choose Subject...";
            //adapter = new SubjectAdapter(Activity, subject);
            //adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            //adapter.SetNotifyOnChange(true);
            adapter = new SubjectSpinnerAdapter(Activity, subject);
            spinner.Adapter = adapter;
            spinner.ItemSelected += Spinner_ItemSelected;

        }

        private void Spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var spinner = (Spinner)sender;
            string toast = string.Format(spinner.GetItemIdAtPosition(e.Position).ToString());
            Toast.MakeText(Activity, toast, ToastLength.Long).Show();
        }

        //private void Spinner_Click(object sender, EventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        //private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        //{
        //    var spinner = sender as Spinner;
        //    //Toast.MakeText(Activity, "You chosed:" + spinner.GetItemAtPosition(e.Position), ToastLength.Short).Show();
        //    Toast.MakeText(Activity, "You chosed:" + spinner.Adapter.GetItem(e.Position), ToastLength.Short).Show();
        //}

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            return inflater.Inflate(Resource.Layout.fragment_students, container, false);

            //return base.OnCreateView(inflater, container, savedInstanceState);
        }
    }
}