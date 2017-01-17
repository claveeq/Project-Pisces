using Android.App;
using Android.Widget;
using Android.OS;
using System.Net;
using System.Net.Sockets;
using System;
using System.Text;
using Android.Support.Design.Widget;

namespace ThesisClient
{
    [Activity(Label = "ThesisClient",
        MainLauncher = true,
        Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        EditText txtIPAddress;
        Button btnconnect;
        Button btnjoin;
        Student student;
        private static readonly Socket ClientSocket = new Socket
            (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        private const int PORT = 8080;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            //
            txtIPAddress = FindViewById<EditText>(Resource.Id.textEditIPAddress);
            btnconnect = FindViewById<Button>(Resource.Id.buttonConnect);
            btnjoin = FindViewById<Button>(Resource.Id.buttonJoin);
           
            //
            btnconnect.Click += Btnconnect_Click;
            btnjoin.Click += Btnjoin_Click;
            //
            student = new Student("Clave", "1234", "1234");
            
 

        }

        private void Btnjoin_Click(object sender, EventArgs e)
        {
            ClientController.Student = student;
            ClientController.context = this;
            ClientController.SendRequest(Task.login);

        }

        private void Btnconnect_Click(object sender, EventArgs e)
        {
            ClientController.ConnectToServer(btnconnect);
        }
    }
}

