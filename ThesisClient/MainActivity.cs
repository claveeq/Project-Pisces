using Android.App;
using Android.Widget;
using Android.OS;
using System.Net;
using System.Net.Sockets;
using System;
using System.Text;
using Android.Support.Design.Widget;
using ThesisClient.Activities;

namespace ThesisClient
{
    [Activity(Label = "Student Passcode")]
    public class MainActivity2 : Activity
    {
        EditText txtPasscode;
        Button btnLogin;
     
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Account);
            //
            txtPasscode = FindViewById<EditText>(Resource.Id.editTextPasscode);
            btnLogin = FindViewById<Button>(Resource.Id.buttonLogin);

            //
            btnLogin.Click += BtnLogin_Click;
            //
            //student = new Student("Clave", "1234", "1234");
           
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(DashboardActivity));
        }

        //private void Btnjoin_Click(object sender, EventArgs e)
        //{
        //    ClientController.Student = student;
        //    ClientController.context = this;
        //    ClientController.SendRequest(Task.login);

        //}

        //    private void Btnconnect_Click(object sender, EventArgs e)
        //    {
        //        ClientController.ConnectToServer(btnconnect);
        //    }
    }
}

