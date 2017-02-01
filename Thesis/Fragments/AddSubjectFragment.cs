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
using Android.Support.Design.Widget;

namespace Thesis.Fragments
{
    public class AddSubjectFragment : Fragment
    {
        EditText editSubject;
        Button btnAddSubject;
        SubjectFragment subjectFragment;
        DashboardActivity DashActivity;
        ClassroomManager classManager;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your fragment here
        }
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            editSubject = View.FindViewById<EditText>(Resource.Id.editTextSubject);
            btnAddSubject = View.FindViewById<Button>(Resource.Id.buttonAddSubject);
            subjectFragment = FragmentManager.FindFragmentByTag<SubjectFragment>("Subject");

            btnAddSubject.Click += BtnAddSubject_Click;
            DashActivity = (DashboardActivity)Activity;//communicating with activities
            classManager = DashActivity.GetClassManager;
        }

        private void BtnAddSubject_Click(object sender, EventArgs e)
        {
            if(!Auth.SubjectExist(editSubject.Text))
            {
                Subject subject = new Subject(editSubject.Text, classManager.GetTeacher.GetID);
                classManager.CurrentSubject = subject;
                classManager.AddSubject(subject);
                editSubject.Text = string.Empty;
                Snackbar.Make(btnAddSubject, "Successfully Added!", Snackbar.LengthShort).Show();
                DashActivity.ReplaceFragment(DashActivity.subjectFragment);
            }
            else
            {
                Snackbar.Make(btnAddSubject, "Subject already exist!", Snackbar.LengthShort).Show();
            }
          

        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
             return inflater.Inflate(Resource.Layout.fragment_create_subject, container, false);

            // return base.OnCreateView(inflater, container, savedInstanceState);
        }
    }
}