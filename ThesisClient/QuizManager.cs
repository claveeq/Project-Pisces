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

namespace ThesisClient
{
    public class QuizManager
    {
        string studentPasscode;
        string title;
        List<QuizItem> items;
        int currentNo;

        public string GetTitle { get{ return title; } }
        public int TotalNoItems
        {
            get { return items.Count; }
        }

        public QuizItem CurrentItem
        {
            get
            {
                
                var currentItem = items.Where(x => x.ItemNo == currentNo).FirstOrDefault();
                return currentItem;
            }
        }

        public QuizManager(string studentPasscode, string title, List<QuizItem> items)
        {
            this.studentPasscode = studentPasscode;
            this.title = title;
            this.items = items;
        }
        public void StartQuiz()
        {
            currentNo = 1;

        }
        public bool NextQuestion()
        {
            currentNo += 1;
            var currentItem = items.Where(x => x.ItemNo == currentNo).FirstOrDefault();
            if(currentItem == null)//return true if there's no more question;
                return false;
            return true;
        }
        public void SendQuiz()
        {
            ClientController.doneQuizData = new QuizData(title, items);
            ClientController.SendRequest(Task.quizDone);
        } 
    }
}