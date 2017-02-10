using Android.App;
using Android.Content;
using Android.Net.Wifi;
using Android.Widget;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Timers;
using Thesis.Model;

namespace Thesis
{
    enum task { login, quiz, assignments, letures, none, exit,
        quizAccept
    }
    
    static class ServerController
    {

        private static readonly Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp);
        private static readonly List<Socket> clientSockets = new List<Socket>();
        private static readonly List<AuthStudent> Students = new List<AuthStudent>();
        private static readonly List<AuthStudent> UnregStudents = new List<AuthStudent>();
        private const int BUFFER_SIZE = 2048;
        private const int PORT = 8080; //NEW 100 before
        private static readonly byte[] buffer = new byte[BUFFER_SIZE];
        
        private static task currentTask = task.none;
        public static ClassroomManager classManager;
        public static Context context;
        public static System.Timers.Timer timer;
        public static bool isLate = false;
        //public static  AttendanceTimer timer;
        //QUIZ
        public static QuizData quizData;
        public static List<AuthStudent> GetActiveStudents {
            get { return Students; }
        }

        public static string GetIPAddress(Context context)
        {

            WifiManager wifiManager = (WifiManager)context.GetSystemService(Service.WifiService);
            int ip = wifiManager.ConnectionInfo.IpAddress;
#pragma warning disable CS0618 // Type or member is obsolete
            var ipaddress = Android.Text.Format.Formatter.FormatIpAddress(ip);
#pragma warning restore CS0618 // Type or member is obsolete
            if(ipaddress == "0.0.0.0")
                return "You're not connected to the internet";
            return ipaddress;
        }
        public static bool FireUp(string ip)
        {
            //  status.Text += "Setting up server..." + Environment.NewLine;
            //Custom IP
            try
            {
                IPAddress ipaddress = IPAddress.Parse(ip);
                serverSocket.Bind(new IPEndPoint(ipaddress, PORT));

                //Establishing a Server Socket Connection
                //serverSocket.Bind(new IPEndPoint(IPAddress.Any, PORT));
                //listen to incoming client connection
                serverSocket.Listen(0);
                //Accepting client

                serverSocket.BeginAccept(AcceptCallback, null);
                //timer = new AttendanceTimer(60);
                //timer.start();
               
                return true;
            }
            catch(Exception)
            {

                return false;
            }
          
         //   status.Text += "Server setup complete" + Environment.NewLine;
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

           // Console.WriteLine("Client connected, waiting for request...");

            socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, ReceiveCallback, socket);
            
            //Accept incoming connection from different devices
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
                //txtbx.Text += "Client forcefully disconnected" + Environment.NewLine;
                // Don't shutdown because the socket may be disposed and its disconnected anyway.
                current.Close();
                clientSockets.Remove(current);
              //  Students.Remove(student);
                return;
            }

            byte[] recBuf = new byte[received];
            Array.Copy(buffer, recBuf, received);
            
            //Conditional test for Changing Task
            if(currentTask == task.none)
            {
                string text = Encoding.ASCII.GetString(recBuf);
                //txtbx.Text += "Received Text: " + text + Environment.NewLine;

                if(text.ToLower() == "login") // Client request an authentication
                {
                    currentTask = task.login;
                    //txtbx.Text += "sent: ok" + Environment.NewLine;
                    byte[] data = Encoding.ASCII.GetBytes("ok");
                    current.Send(data);  

                }
                else if(text.ToLower() == "quiz") // Send ok and Send Quiz
                {
                    if(quizData != null)
                    {      
                        byte[] data = Encoding.ASCII.GetBytes("yes");
                        current.Send(data);
                    }
                    else
                    {
                        byte[] data = Encoding.ASCII.GetBytes("no");
                        current.Send(data);
                    }
                }
                else if(text.ToLower() == "quizaccept")
                {
                    byte[] data = Encoding.ASCII.GetBytes("ok");
                    current.Send(data);
                    currentTask = task.quizAccept;
                }
                else if(text.ToLower() == "exit") // Client wants to exit gracefully
                {
                    // Always Shutdown before closing
                    current.Shutdown(SocketShutdown.Both);
                    current.Close();
                    clientSockets.Remove(current);
                    //Console.WriteLine("Client disconnected");
                    return;
                }
                else
                {
                    Console.WriteLine("Text is an invalid request");
                    byte[] data = Encoding.ASCII.GetBytes("Invalid request");
                    current.Send(data);
                    Console.WriteLine("Warning Sent");
                }
            }
            else // this is for initializing task
            {
                //Login
                if(currentTask == task.login)
                {
                    AuthStudent student;
                    student = (AuthStudent)BinarySerializer.ByteArrayToObject(recBuf);
                    if(Auth.AuthStudent(student))
                    {
                        var teachersstudent = classManager.GetSubjectStudents.Find(x => x.GetPasscode == student.GetPasscode);
                        if(isLate) //setting the student status to present
                            teachersstudent.Status = 3;
                        else
                            teachersstudent.Status = 2;

                        if(!Students.Contains(student))// If doesn't exist in the list, Add it to Regestered list
                            Students.Add(student);

                        current.Send(Encoding.ASCII.GetBytes("true"));
                    }
                    else
                    {
                        if(!Students.Contains(student)) // If doesn't exist in the list, Add it to Unregestered list
                            UnregStudents.Add(student);

                        current.Send(Encoding.ASCII.GetBytes("false"));
                    }
                    currentTask = task.none;
                }
                //Quiz
                else if(currentTask == task.quizAccept)
                {
                    string text = Encoding.ASCII.GetString(recBuf);
                    if(text == "accepted")
                    {
                        //byte[] quizdata = BinarySerializer.QuiztoByteArray(quizData);
                        //var ndew = BinarySerializer.ByteArrayToQuiz(quizdata);
                    
                       var json = JsonConvert.SerializeObject(quizData);
                       //var account = JsonConvert.DeserializeObject<QuizData>(json);
          
                        // var item = JsonConvert.DeserializeObject<QuizData>(json);
                        current.Send(Encoding.ASCII.GetBytes(json));

                    }
                    else if(text == "done")
                    {
                        currentTask = task.none;
                    } 
                }
            }
            current.BeginReceive(buffer, 0, BUFFER_SIZE, SocketFlags.None, ReceiveCallback, current);
        }

        //Closing all sockts

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
    }
}

