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
using System.Collections;
using System.IO;
using Thesis.Model;
using Newtonsoft.Json;

namespace Thesis
{
    public enum quizitemNavigation { next, previous, add ,delete}
    /*The most important thing in this object is to set the field quiz because
     * to avoid error. The said field is commonly used in most of the methods;
    */
    public class QuizManager
    {
        Quiz quiz;
        
        int _teachersID; //get the teachers id
        DateTime _dateCreated;
        List<string> quiznames;
        List<StudentQuizScore> scores = new List<StudentQuizScore>();
        public int currentItemNo = 1;

        public Quiz Quiz { get { return quiz; } }

        public   List<CorrectAnswers> correct;

        public QuizManager(int teachersid)
        {
            _teachersID = teachersid;
            _dateCreated = DateTime.Now;
            quiznames = new List<string>();
        }

        public void CreateQuiz(string title, string subject)
        {
            quiz = new Quiz(_teachersID, title, _dateCreated, new List<QuizItem>(), subject);
        }

        public List<Subject> GetTeachersSubjects()
        {
            return DBManager.GetTeachersSubjects(_teachersID);
        }
        public QuizItem itemNavigation(quizitemNavigation task)
        {
            switch(task)
            {
                case quizitemNavigation.next:
                    currentItemNo += 1;
                    break;
                case quizitemNavigation.previous:
                    if(1 < currentItemNo)
                    currentItemNo -= 1;
                    break;
                case quizitemNavigation.add:
                    currentItemNo = GetQuizItems.Count + 1;
                    break;
                case quizitemNavigation.delete:
                    //if(1 < currentItemNo)
                    //    currentItemNo -= 1;
                    break;
            }
            QuizItem quizitem = Quiz.GetQuizitems.Where(x => x.ItemNo == currentItemNo).FirstOrDefault();
            return quizitem;
        }

        internal void EndQuiz()
        {
            currentItemNo = 1;
        }

        public void DeleteItem(int itemNo)
        {
            var item = quiz.GetQuizitems.Where(x => x.ItemNo == itemNo).FirstOrDefault();
            quiz.GetQuizitems.Remove(item);
            int y = 1;
            foreach(var quizitem in quiz.GetQuizitems)
            {
                quizitem.ItemNo = y++;
            }
        }

        public void StartQuiz()
        {
            QuizData quizdata = new QuizData(quiz.Title, Quiz.GetQuizitems, true);
            ServerController.quizData = quizdata; //to start the quiz, set it to the server
        }
        public List<StudentQuizScore> CheckQuiz()
        {
            scores.Clear();
            foreach(var item in ServerController.QuizDoneItems)
            {
                StudentQuizScore student = new StudentQuizScore(item.Passcode, _teachersID, item.items, quiz.GetQuizitems);
                student.CheckItem(correct);
                scores.Add(student);
            }
            return scores;
        }
        public void AddItem(string question, string a, string b, string c, string d, string answer)
        {
            int number = GetQuizItems.Count + 1;
            QuizItem newquizitem = new QuizItem(number, question, a, b, c, d, answer);
            quiz.AddItem(newquizitem);
        }

        public List<QuizItem> GetQuizItems { get { return quiz.GetQuizitems; } }

        public void SerializeQuiz()
        {
            BinarySerializer.QuizObjToDataFile(quiz);
        }

        public void DeserializeQuiz(string filename)
        {
            correct = new List<CorrectAnswers>();
            quiz = BinarySerializer.DataFileToQuizObj(filename);
            foreach(var item in quiz.GetQuizitems)
            {
                correct.Add(new CorrectAnswers(item.ItemNo,item.Answer));
            }
        }
        
        public List<string> GetAllQuizzes()
        {
            string folderlocation;
            if(Android.OS.Environment.ExternalStorageState.Equals(Android.OS.Environment.MediaMounted))
                folderlocation = Android.OS.Environment.ExternalStorageDirectory.Path;
            else
                folderlocation = Android.OS.Environment.DirectoryDocuments;

            folderlocation += @"/Quizzes/";
            if(!Directory.Exists(folderlocation))
                Directory.CreateDirectory(folderlocation);

           // string file = @"/*.dat";

            string filepath = folderlocation;
            //DirectoryInfo d = new DirectoryInfo(folderlocation);
            string[] files = Directory.GetFiles(folderlocation);
            if(files != null)
            {
                foreach(var file in files)
                {
                    FileInfo fi = new FileInfo(file);
                  //  fi.Name.Replace(fi.Extension, "");
                    quiznames.Add(fi.Name.Replace(fi.Extension, ""));
                }
            }
            return quiznames;
        }
    }
}