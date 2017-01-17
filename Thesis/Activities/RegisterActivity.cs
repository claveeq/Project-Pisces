using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.IO;
using SQLite;
using Thesis.Table;

namespace Thesis.Activities
{
    [Activity(Label = "RegisterActivity")]
    public class RegisterActivity : Activity
    {
        EditText txtusername;
        EditText txtPassword;
        Button btncreate;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Newuser);
            // Create your application here  
            btncreate = FindViewById<Button>(Resource.Id.btnRegCreate);
            txtusername = FindViewById<EditText>(Resource.Id.txtRegUsername);
            txtPassword = FindViewById<EditText>(Resource.Id.txtRegPassword);
            // EventHandlers
            btncreate.Click += Btncreate_Click;
        }

        private void Btncreate_Click(object sender, EventArgs e)
        {
            Teacher teacher = new Teacher(txtusername.Text, txtPassword.Text);
            if(Auth.CreateTeacher(teacher, Application.Context))
            {
                Finish();
            } 
        }
    }
}