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

namespace Thesis
{
    class ServerController
    {

        IPAddress iPAddress;
        TcpListener server;
        TcpClient client;


        public string Start(Context context, string ip)
        {
            iPAddress = IPAddress.Parse(ip);
            server = new TcpListener(iPAddress, 8080);
            client = default(TcpClient);

            string status;

            try
            {
                server.Start();
                status = "Server is running...";
                return status;
            }
            catch(Exception ex)
            {
                status = ex.ToString();
                return status;
            }
        }      
        
        public void connection()
        {
            while(true)
            {
                client = server.AcceptTcpClient();
                byte[] receivedBuffer = new byte[100];
                NetworkStream stream = client.GetStream();

                stream.Read(receivedBuffer, 0, receivedBuffer.Length);

                StringBuilder msg = new StringBuilder();

                foreach(byte b in receivedBuffer)
                {
                    if(b.Equals(00))
                        break;
                    else
                        msg.Append(Convert.ToChar(b).ToString());
                }
                Console.WriteLine(msg.ToString() + msg.Length);
            }
        }
    }
}