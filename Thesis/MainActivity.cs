using Android.App;
using Android.Widget;
using Android.OS;
using System;
using SQLite;
using System.IO;
using Android.Views;
using System.Net;
using System.Linq;
using System.Net.Sockets;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using Android.Support.Design.Widget;
using Android.Content;
using Newtonsoft.Json;
namespace Thesis
{
    [Activity(Label = "Thesis", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    { 
        EditText txtusername;
        EditText txtPassword;
        Button btncreate;
        Button btnsign;
        TextView labelLogin;


        Teacher teacher;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // for changing the color of the status bar
            Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Authentication);
            // Get our button from the layout resource,  
            InitViews();
            // and attach an event to it  
            EventHanlders();
            //init
            ServerController.context = Application.Context;
            Auth.Init();
        }
        private void InitViews()
        {
            btnsign = FindViewById<Button>(Resource.Id.btnLogin);
            btncreate = FindViewById<Button>(Resource.Id.btnRegister);
            txtusername = FindViewById<EditText>(Resource.Id.txtUsername);
            txtPassword = FindViewById<EditText>(Resource.Id.txtPassword);
            labelLogin = FindViewById<TextView>(Resource.Id.labelLogin);
        }

        private void EventHanlders()
        {
            btnsign.Click += Btnsign_Click;
            btncreate.Click += Btncreate_Click;
        }

        private void Btncreate_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(Activities.RegisterActivity));
        }

        private void Btnsign_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(Activities.DashboardActivity));
            if(Auth.AuthTeacher(txtusername.Text, txtPassword.Text))
            {
                intent.PutExtra("username", txtusername.Text);
                intent.PutExtra("password", txtPassword.Text);
                StartActivity(intent);          
                Finish();
            }
            else
            {
                teacher = null;
                Snackbar.Make(btnsign, "Account doesn't exist", 0).Show();
            }
        }
    }
}

