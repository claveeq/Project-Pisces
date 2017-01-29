using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Android.Widget;
using Android.Support.Design.Widget;
using Android.Views;
using Android.App;
using Android.Content;

namespace ThesisClient
{ 
    enum Task { login, none, exit}

    static class ClientController
    {  
        private static readonly Socket ClientSocket = new Socket
            (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        private const int PORT = 8080;
        //
        public static AuthStudent Student;
        public static Context context;

        private static Task currentTask = Task.none;

        public static bool ConnectToServer(string iPddress)
        {
            //int attempts = 0;

            //while(!ClientSocket.Connected)
            //{
                try
                {
                    //attempts++;
                   // btn.Text = "Connection attempt " + attempts;
                    // Change IPAddress.Loopback to a remote IP to connect to a remote host.
                    IPAddress ipaddress = IPAddress.Parse(iPddress);
                    ClientSocket.Connect(ipaddress, PORT);
                    return true;
                    //connect to the listening server
                    //ClientSocket.Connect(IPAddress.Loopback, PORT);

                }
                catch(Exception ex)
                {
               // Toast toast = Toast.MakeText(context, ex.ToString(), ToastLength.Long);
                // btn.Text = "Error!";
                return false;
                }
            //}
            //btn.Text = "Connected!";
            //btn.Enabled = false;
        }

        //private static void RequestLoop()
        //{
        //    Console.WriteLine(@"<Type ""exit"" to properly disconnect client>");

        //    while(true)
        //    {
        //        SendRequest();
        //        ReceiveResponse(task);
        //    }
        //}


        public static void SendRequest(Task task)
        {
            if(task == Task.login)
            {
                currentTask = Task.login;
                SendString("login");
                ReceiveResponse(currentTask);
            }
            if(task == Task.exit)
            {
                Exit();
            }
           // Student person = new Student("1", 1, "1", "1");
        }
        /// <summary>
        /// Sends a string to the server with ASCII encoding.
        /// </summary>
        private static void SendString(string text)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(text);
            ClientSocket.Send(buffer, 0, buffer.Length, SocketFlags.None);
        }
        private static void SendObject(object obj)
        {
            byte[] buffer = BinarySerializer.ObjectToByteArray(obj);
            ClientSocket.Send(buffer, 0, buffer.Length, SocketFlags.None);
        }

        public static void ReceiveResponse(Task current)
        {
            var buffer = new byte[2048];
            int received = ClientSocket.Receive(buffer, SocketFlags.None);
            if(received == 0)
                return;
            var data = new byte[received];
            Array.Copy(buffer, data, received);
            //Task to do if it's ready to send another data
            if(current == Task.login)
            {
                string text = Encoding.ASCII.GetString(data);
                if(text.ToLower() == "ok")
                {
                    SendObject(Student);
                    ReceiveResponse(Task.login);
                }
                else if(text.ToLower() == "true")
                {
                    //Toast toast = Toast.MakeText(context, "You are know in the class.",ToastLength.Long);
                    //toast.Show();
                    currentTask = Task.none;
                }
                else if(text.ToLower() == "false")
                {
                    //Toast toast = Toast.MakeText(context, "You are know in the class.", ToastLength.Long);
                    //toast.Show();
                    currentTask = Task.none;
                }
            }
            else
            {
                string text = Encoding.ASCII.GetString(data);
                //Console.WriteLine(text);
            }
        }
 
        /// <summary>
        /// Close socket and exit program.
        /// </summary>
        private static void Exit()
        {
            SendString("exit"); // Tell the server we are exiting
            ClientSocket.Shutdown(SocketShutdown.Both);
            ClientSocket.Close();
            Environment.Exit(0);
        }
    }
}
