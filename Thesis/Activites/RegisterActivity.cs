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
            btncreate.Click += Btncreate_Click;
        }

        private void Btncreate_Click(object sender, EventArgs e)
        {
            try
            {
                string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "user.db3");
                var db = new SQLiteConnection(dpPath);
                db.CreateTable<LoginTable>();
                LoginTable tbl = new LoginTable();
                tbl.username = txtusername.Text;
                tbl.password = txtPassword.Text;
                db.Insert(tbl);
                Toast.MakeText(this, "Record Added Successfully...,", ToastLength.Short).Show();
            }
            catch(Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }
        }
    }
}