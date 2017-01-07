using Android.App;
using Android.Widget;
using Android.OS;
using System.Net;
using System.Net.Sockets;
using System;
using System.Text;

namespace ThesisClient
{
    [Activity(Label = "ThesisClient", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {

        Button btnstart;
        Button btnsend;
        TextView txtstatus;
        EditText txtmessage;

        private static readonly Socket ClientSocket = new Socket
            (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        private const int PORT = 8080;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);
            //
            btnstart = FindViewById<Button>(Resource.Id.buttonStart);
            btnsend = FindViewById<Button>(Resource.Id.buttonSend);
            txtstatus = FindViewById<TextView>(Resource.Id.textViewStatus);
            txtmessage = FindViewById<EditText>(Resource.Id.textEditMessage);
            //
            btnstart.Click += Btnstart_Click;
            btnsend.Click += Btnsend_Click;

            //ConnectToServer();
            //SendRequest();
            //ReceiveResponse();
            //Exit();

        }

        private void Btnsend_Click(object sender, EventArgs e)
        {
            SendRequest(txtmessage);
        }

        private void Btnstart_Click(object sender, EventArgs e)
        {
            ConnectToServer(txtstatus);
        }

        private static void ConnectToServer(TextView status)
        {
            int attempts = 0;

            while(!ClientSocket.Connected)
            {
                try
                {
                    attempts++;
                    //Console.WriteLine("Connection attempt " + attempts);
                    status.Text = "Connection attempt " + attempts;
                    // Change IPAddress.Loopback to a remote IP to connect to a remote host.
                    IPAddress ipaddress = IPAddress.Parse("192.168.254.100");
                    ClientSocket.Connect(ipaddress, PORT);
                    //ClientSocket.Connect(IPAddress.Loopback, PORT);

                }
                catch(SocketException)
                {
                    //Console.Clear();
                }
            }

            //Console.Clear();
            //Console.WriteLine("Connected");
            status.Text = "Connected";
        }

        /// <summary>
        /// Close socket and exit program.
        /// </summary>
        private static void Exit()
        {
            SendString("exit"); // Tell the server we are exiting
            ClientSocket.Shutdown(SocketShutdown.Both);
            ClientSocket.Close();
            System.Environment.Exit(0);
        }

        private static void SendRequest(EditText message)
        {
            
            //Console.Write("Send a request: ");
            //string request = Console.ReadLine();
            string request = message.Text;
            SendString(request);

            if(request.ToLower() == "exit")
            {
                Exit();
            }
        }

        /// <summary>
        /// Sends a string to the server with ASCII encoding.
        /// </summary>
        private static void SendString(string text)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(text);
            ClientSocket.Send(buffer, 0, buffer.Length, SocketFlags.None);
        }

        private static void ReceiveResponse()
        {
            var buffer = new byte[2048];
            int received = ClientSocket.Receive(buffer, SocketFlags.None);
            if(received == 0)
                return;
            var data = new byte[received];
            Array.Copy(buffer, data, received);
            string text = Encoding.ASCII.GetString(data);
            Console.WriteLine(text);
        }
    }
}

