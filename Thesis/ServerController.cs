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
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using Android.Net.Wifi;
using Android.Text.Format;
namespace Thesis
{
     class ServerController
    {
        private static readonly Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp);
        private static readonly List<Socket> clientSockets = new List<Socket>();
        private const int BUFFER_SIZE = 2048;
        private const int PORT = 8080;
        private static readonly byte[] buffer = new byte[BUFFER_SIZE];

        //TcpListener server;
        //TcpClient client;


        //public string Start(Context context)
        //{
        //    string status;

        //    try
        //    {
        //        WifiManager wifiManager = (WifiManager)context.GetSystemService(Service.WifiService);
        //        int ip = wifiManager.ConnectionInfo.IpAddress;
        //        string ipFormat = Formatter.FormatIpAddress(ip);
        //        IPAddress ipaddress = IPAddress.Parse(ipFormat);


        //        server = new TcpListener(ipaddress, 8080);
        //        client = default(TcpClient);

        //        server.Start();
        //        status = "Server is running on "+ ipaddress.ToString();
        //        return status;
        //    }
        //    catch(Exception ex)
        //    {
        //        status = ex.ToString();
        //        return status;
        //    }
        //}

        //public void StartMessage()
        //{
        //    while(true)
        //    {
        //        client = server.AcceptTcpClient();
        //        byte[] receivedBuffer = new byte[100];
        //        NetworkStream stream = client.GetStream();

        //        stream.Read(receivedBuffer, 0, receivedBuffer.Length);

        //        StringBuilder msg = new StringBuilder();

        //        foreach(byte b in receivedBuffer)
        //        {
        //            if(b.Equals(00))
        //                break;
        //            else
        //                msg.Append(Convert.ToChar(b).ToString());
        //        }
        //        Console.WriteLine(msg.ToString() + msg.Length);
        //    }
        //}


        public static void SetupServer(TextView textstatus)
        {

            //Console.WriteLine("Setting up server...");
            //Toast toast = Toast.MakeText(context, "SEttingu up server...", ToastLength.Long);
            //toast.Show();
            textstatus.Text = "Setting up server...";
;            // new
            IPAddress ipaddress = IPAddress.Parse("192.168.254.104");
            serverSocket.Bind(new IPEndPoint(ipaddress, PORT));
            //serverSocket.Bind(new IPEndPoint(IPAddress.Any, PORT));
            serverSocket.Listen(0);
            serverSocket.BeginAccept(AcceptCallback, null);
            textstatus.Text = "Server setup complete";
            //Console.WriteLine("Server setup complete");
        }

        /// <summary>
        /// Close all connected client (we do not need to shutdown the server socket as its connections
        /// are already closed with the clients).
        /// </summary>
        public static void CloseAllSockets()
        {
            foreach(Socket socket in clientSockets)
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }

            serverSocket.Close();
        }

        private static void AcceptCallback(IAsyncResult AR)
        {
            Socket socket;

            try
            {
                socket = serverSocket.EndAccept(AR);
            }
            catch(ObjectDisposedException) // I cannot seem to avoid this (on exit when properly closing sockets)
            {
                return;
            }

            clientSockets.Add(socket);
            socket.BeginReceive(buffer, 0, BUFFER_SIZE, SocketFlags.None, ReceiveCallback, socket);
            Console.WriteLine("Client connected, waiting for request...");
            serverSocket.BeginAccept(AcceptCallback, null);
        }

        private static void ReceiveCallback(IAsyncResult AR)
        {
            Socket current = (Socket)AR.AsyncState;
            int received;

            try
            {
                received = current.EndReceive(AR);
            }
            catch(SocketException)
            {
                Console.WriteLine("Client forcefully disconnected");
                // Don't shutdown because the socket may be disposed and its disconnected anyway.
                current.Close();
                clientSockets.Remove(current);
                return;
            }

            byte[] recBuf = new byte[received];
            Array.Copy(buffer, recBuf, received);
            string text = Encoding.ASCII.GetString(recBuf);
            Console.WriteLine("Received Text: " + text);

            if(text.ToLower() == "get time") // Client requested time
            {
                Console.WriteLine("Text is a get time request");
                byte[] data = Encoding.ASCII.GetBytes(DateTime.Now.ToLongTimeString());
                current.Send(data);
                Console.WriteLine("Time sent to client");
            }
            else if(text.ToLower() == "exit") // Client wants to exit gracefully
            {
                // Always Shutdown before closing
                current.Shutdown(SocketShutdown.Both);
                current.Close();
                clientSockets.Remove(current);
                Console.WriteLine("Client disconnected");
                return;
            }
            else
            {
                Console.WriteLine("Text is an invalid request");
                byte[] data = Encoding.ASCII.GetBytes("Invalid request");
                current.Send(data);
                Console.WriteLine("Warning Sent");
            }

            current.BeginReceive(buffer, 0, BUFFER_SIZE, SocketFlags.None, ReceiveCallback, current);
        }
    }
}