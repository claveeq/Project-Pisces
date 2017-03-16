using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Android.Widget;
using Android.Support.Design.Widget;
using Android.Views;
using Android.App;
using Android.Content;
using Newtonsoft.Json;
using System.Collections.Generic;
using ThesisClient.Model;
using System.IO;

namespace ThesisClient
{ 
    enum Task { login, quiz, none, exit,
        quizAccept,
        quizDone,
        assignment,
        lecture,
        lectureAccept
    }

    static class ClientController
    {  
        private static readonly Socket ClientSocket = new Socket
            (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        private const int PORT = 8080;
        //
        public static AuthStudent Student;
        public static Context context;

        private static Task currentTask = Task.none;

        public static QuizData quiz;
        public static bool QuizIsAvailable = false;
        //quiz items
        public static QuizData quizData;
        public static List<QuizItem> quizItem;
        public static QuizData doneQuizData;
        public static TextView tvscores;
        //Assignment;
        public static List<Assignment> assignments = new List<Assignment>();
        //Lectures;
        public static string lectureFileName;
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
            else if(task == Task.quiz)
            {
                currentTask = Task.quiz;
                SendString("quiz");
                ReceiveResponse(currentTask);
            }
            else if(task == Task.quizDone)
            {
                currentTask = Task.quizDone;
                SendString("quizdone");
                ReceiveResponse(currentTask);
            }
            else if(task == Task.assignment)
            {
                currentTask = Task.assignment;
                SendString("assignment");
                ReceiveResponse(currentTask);
            }
            else if(task == Task.lecture)
            {
                currentTask = Task.lecture;
                SendString("lecture");
                ReceiveResponse(currentTask);
            }
            else if(task == Task.exit)
            {
                Exit();
            }
           // Student person = new Student("1", 1, "1", "1");
        }
      
        public static void ReceiveResponse(Task current)
        {

            var buffer = new byte[50000];
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
            else if(current == Task.quiz)//1st step of sending quiz
            {
                string text = Encoding.ASCII.GetString(data);
                if(text.ToLower() == "yes")
                {
                    SendString("quizaccept");
                    ReceiveResponse(Task.quiz);
                }
                else if(text.ToLower() == "no")
                {
                    QuizIsAvailable = false;
                    currentTask = Task.none;
                }
                else if (text.ToLower() == "ok")
                {
                    SendString("accepted");
                    ReceiveResponse(Task.quizAccept);
                }
            }
            else if(current == Task.quizAccept)//2nd step ofo sending quiz
            {
                // quiz = BinarySerializer.ByteArrayToQuiz(buffer);
                string json = Encoding.ASCII.GetString(data);
                quizData = JsonConvert.DeserializeObject<QuizData>(json);
                quizItem = JsonConvert.DeserializeObject<List<QuizItem>>(quizData.quizitems);
   
                SendString("done");
                QuizIsAvailable = true;
                currentTask = Task.none;
            }
            else if(current == Task.quizDone)//2nd step ofo sending quiz
            {
                string text = Encoding.ASCII.GetString(data);
                if(text.ToLower() == "ok")
                {
                    var json = JsonConvert.SerializeObject(doneQuizData);
                    SendString(json);
                    ReceiveResponse(Task.quizDone);
                }
                else if(text.ToLower() == "false")
                {
                    currentTask = Task.none;
                }
                else
                {
                    tvscores.Text = text;
                    currentTask = Task.none;
                }
            }
            else if(current == Task.assignment)//2nd step ofo sending quiz
            {
                string text = Encoding.ASCII.GetString(data);
                if(text.ToLower() == "ok")
                {
                    SendString("true");
                    ReceiveResponse(Task.assignment);
                }
                else
                {
                    assignments = JsonConvert.DeserializeObject<List<Assignment>>(text);
                    currentTask = Task.none;
                }
            }
            else if(current == Task.lecture)//2nd step ofo sending quiz
            {  
                if(Encoding.ASCII.GetString(data).ToLower() == "ok")
                {
                    SendString("true");
                    ReceiveResponse(Task.lecture);
                }
                else
                {
                    //  assignments = JsonConvert.DeserializeObject<List<Assignment>>(text);
                    lectureFileName = Encoding.ASCII.GetString(data);
                    SendString("acceptlect");
                    ReceiveResponse(Task.lectureAccept);
                }
            }
            else if(current == Task.lectureAccept)//2nd step ofo sending quiz
            {
                BinarySerializer.ByteArrayToFile(lectureFileName, data);
                lectureFileName = string.Empty;
                currentTask = Task.none;
            }
            else
            {
                string text = Encoding.ASCII.GetString(data);
                currentTask = Task.none;
                //Console.WriteLine(text);
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
        private static void SendObject(object obj)
        {
            byte[] buffer = BinarySerializer.ObjectToByteArray(obj);
            ClientSocket.Send(buffer, 0, buffer.Length, SocketFlags.None);
        }
        /// <summary>
        /// Close socket and exit program.
        /// </summary>
        public static void Exit()
        {
            SendString("exit"); // Tell the server we are exiting
            ClientSocket.Shutdown(SocketShutdown.Both);
            ClientSocket.Close();         
        }
    }
}
