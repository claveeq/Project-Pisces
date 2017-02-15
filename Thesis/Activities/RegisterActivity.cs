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
using Android.Support.Design.Widget;

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
            if(txtUsername.Text == string.Empty || txtPassword.Text == string.Empty || txtFullname.Text == string.Empty)
            {
                Snackbar.Make(btncreate, "Please fill in the required fields.", Snackbar.LengthShort).Show();
                return;
            }
            if(Auth.TeacherExist(txtUsername.Text))
            {
                Snackbar.Make(btncreate, "The username you have entered is already exist in this app. Try a different username.", Snackbar.LengthLong).Show();
                return;
            }
            if(Auth.CreateTeacher(txtUsername.Text, txtPassword.Text, txtFullname.Text))
            {
                var builder = new AlertDialog.Builder(this);
                builder.SetTitle("Registration");
                builder.SetMessage( "Account Successfully Created, "+ txtFullname.Text + "!");
                builder.SetPositiveButton("Let's start", (senderAlert, args) => {
                    Finish();
                });
                Dialog dialog = builder.Create();
                dialog.Show();

            } 
        }
    }
}