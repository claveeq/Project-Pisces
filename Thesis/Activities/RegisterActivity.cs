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
        EditText txtUsername;
        EditText txtPassword;
        EditText txtFullname;
        Button btncreate;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Newuser);
            // Create your application here  
            btncreate = FindViewById<Button>(Resource.Id.btnRegCreate);
            txtUsername = FindViewById<EditText>(Resource.Id.txtRegUsername);
            txtPassword = FindViewById<EditText>(Resource.Id.txtRegPassword);
            txtFullname = FindViewById<EditText>(Resource.Id.txtRegFullName);
            // EventHandlers
            btncreate.Click += Btncreate_Click;
            Theme.ApplyStyle(Resource.Style.MyTheme, true);
        }

        private void Btncreate_Click(object sender, EventArgs e)
        {
            if(Auth.CreateTeacher(txtUsername.Text, txtPassword.Text, txtFullname.Text))
            {
                Finish();
            } 
        }
    }
}