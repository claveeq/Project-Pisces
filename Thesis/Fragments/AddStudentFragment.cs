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
    public class AddStudentFragment : Fragment
    {
        EditText txtfirstName;
        EditText txtlastName;
        EditText txtPasscode;
        Button btnAddStudent;

        DashboardActivity dashActivity;
        ClassroomManager classManager;
        Student student;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            txtfirstName = View.FindViewById<EditText>(Resource.Id.editTextFirstName);
            txtlastName = View.FindViewById<EditText>(Resource.Id.editTextLastName);
            txtPasscode = View.FindViewById<EditText>(Resource.Id.editTextPasscode);
            btnAddStudent = View.FindViewById<Button>(Resource.Id.buttonAddStudent);

            dashActivity = (DashboardActivity)Activity;
            classManager = dashActivity.GetClassManager;

            btnAddStudent.Click += BtnAddStudent_Click;
        }

        private void BtnAddStudent_Click(object sender, EventArgs e)
        {
            if(!Auth.StudentExist(txtPasscode.Text))
            {
                student = new Student(txtPasscode.Text, txtfirstName.Text, txtlastName.Text, classManager.GetTeacher.GetID);            
                classManager.AddStudent(student);
                Snackbar.Make(btnAddStudent, "Successfully Added!", Snackbar.LengthShort).Show();
                dashActivity.ReplaceFragment(dashActivity.studentFragment);
            }
            else
                Snackbar.Make(btnAddStudent, "Student already exist!", Snackbar.LengthShort).Show();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            return inflater.Inflate(Resource.Layout.fragment_add_student, container, false);

            // return base.OnCreateView(inflater, container, savedInstanceState);
        }
    }
}