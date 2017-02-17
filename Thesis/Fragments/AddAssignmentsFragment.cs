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
using Android.Support.Design.Widget;

namespace Thesis.Fragments
{
    public class AddAssignmentsFragment : Fragment
    {
        EditText etTitle;
        Spinner spSubjects;
        EditText etDescription;
        Button btnAssignment;
        SubjectSpinnerAdapter subjectSpinnerAdapter;

        ClassroomManager classManager;
        DashboardActivity dashActivity;

        Subject selectedSubject;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            dashActivity = (DashboardActivity)Activity;
            classManager = dashActivity.GetClassManager;
            etTitle = View.FindViewById<EditText>(Resource.Id.fragment_assignment_etTitle);
            etDescription = View.FindViewById<EditText>(Resource.Id.fragment_assignment_etTitle);
            spSubjects = View.FindViewById<Spinner>(Resource.Id.fragment_assignment_spSubject);
            btnAssignment = View.FindViewById<Button>(Resource.Id.fragment_assignment_btnAssignment);
            subjectSpinnerAdapter = new SubjectSpinnerAdapter(dashActivity, classManager.GetSubjects);
            spSubjects.Adapter = subjectSpinnerAdapter;

            spSubjects.ItemSelected += SpSubjects_ItemSelected;
            btnAssignment.Click += BtnAssignment_Click;
        }

        private void BtnAssignment_Click(object sender, EventArgs e)
        {
            if(etTitle.Text == string.Empty || etDescription .Text == string.Empty || selectedSubject == null)
            {
                Snackbar.Make(btnAssignment, "Please, fill up the required fields above.", Snackbar.LengthShort).Show();
                return;
            }
            classManager.AddAssignments(etTitle.Text,selectedSubject,etDescription.Text);
            dashActivity.ReplaceFragment(dashActivity.assignmentFragment);
        }

        private void SpSubjects_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            selectedSubject =  classManager.GetSubjects[e.Position];

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            View view = inflater.Inflate(Resource.Layout.fragment_assignment_add, container,false);
            return view;
        }
    }
}