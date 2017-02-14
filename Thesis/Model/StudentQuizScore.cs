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
using Thesis.Model;
namespace Thesis.Model
{
    public class StudentQuizScore
    {
        public string passcode;
        public string name;
        int teachersID;
        public int score = 0;
        public List<QuizItem> items;
        List<QuizItem> correctItems;

        public StudentQuizScore(string passcode,int teachersID,  List<QuizItem> items, List<QuizItem> correctItems)
        {
            this.passcode = passcode;
            this.items = items;
            name = getFullNameinDB();
            this.correctItems = correctItems;
        }

        private string getFullNameinDB()
        {
            var student = DBManager.GetStudent(passcode, teachersID);
            return student.GetFirstName + " " + student.GetLastName;
        }

        public void CheckItem(List<CorrectAnswers> correct)
        {
            foreach(var item in items)
            {
                var correctAnswer = correct.Where(x => x.no == item.ItemNo).FirstOrDefault();
                //var correctAnswer = correctItems.Where(x => x.ItemNo == item.ItemNo).FirstOrDefault();
                //if(item.Answer == correctAnswer.Answer)
                if(item.Answer == correctAnswer.answer)
                {
                    ++score;
                }
            }
        }
    }
}