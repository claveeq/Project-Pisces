using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using ThesisClient.Activities;
using System.Timers;

namespace ThesisClient.QuizFragments
{
    public class QuizItemFragment : Android.App.Fragment
    {
        QuizManager quizManager;
        QuizActivity quizActivity;
        TextView tvQuestion;
        TextView tvItemNo;
        TextView tvTimer;
        Button btnA;
        Button btnB;
        Button btnC;
        Button btnD;
        Timer timer;

        int count = 1;
        enum answer { a,b,c,d,
            none
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your fragment here
        }
        private void Timer()//1000 = 1s
        {
            timer = new Timer();
            timer.Interval = 1000;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
            tvTimer.Text = count.ToString();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if(count < 3)
            {
                quizActivity.RunOnUiThread( () => {
                    tvTimer.Text = count.ToString();
                });
             
                count++;
            }
            else
            {
                count = 1;
                itemAnswer(answer.none);
            }
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            quizActivity = Activity as QuizActivity;
            quizManager = quizActivity.quizManager;
            initViews();
            quizManager.StartQuiz();
            populateItem(quizManager.CurrentItem);
            Timer();
            timer.Start();
        }

        private void initViews()
        {
            tvQuestion = View.FindViewById<TextView>(Resource.Id.fragment_quiz_tvQuestion);
            tvItemNo = View.FindViewById<TextView>(Resource.Id.fragment_quiz_tvItemNo);
            tvTimer = View.FindViewById<TextView>(Resource.Id.fragment_quiz_tvTimer);
            btnA = View.FindViewById<Button>(Resource.Id.fragment_quiz_btnA);
            btnB = View.FindViewById<Button>(Resource.Id.fragment_quiz_btnB);
            btnC = View.FindViewById<Button>(Resource.Id.fragment_quiz_btnC);
            btnD = View.FindViewById<Button>(Resource.Id.fragment_quiz_btnD);
            btnA.Click += BtnA_Click;
            btnB.Click += BtnB_Click;
            btnC.Click += BtnC_Click;
            btnD.Click += BtnD_Click;
        }

        private void populateItem(QuizItem item)
        {
            tvItemNo.Text = item.ItemNo + "/" + quizManager.TotalNoItems;
            tvQuestion.Text = item.Question;
            btnA.Text = item.AnsA;
            btnB.Text = item.AnsB;
            btnC.Text = item.AnsC;
            btnD.Text = item.AnsD;
        }

        private void restartItem()
        {
            tvItemNo.Text = string.Empty;
            tvQuestion.Text = string.Empty;
            btnA.Text = string.Empty;
            btnB.Text = string.Empty;
            btnC.Text = string.Empty;
            btnD.Text = string.Empty;
        }

        private void itemAnswer(answer ans)
        {
            switch(ans)
            {
                case answer.a:
                    quizManager.CurrentItem.Answer = quizManager.CurrentItem.AnsA;
                    break;
                case answer.b:
                    quizManager.CurrentItem.Answer = quizManager.CurrentItem.AnsB;
                    break;
                case answer.c:
                    quizManager.CurrentItem.Answer = quizManager.CurrentItem.AnsC;
                    break;
                case answer.d:
                    quizManager.CurrentItem.Answer = quizManager.CurrentItem.AnsD;
                    break;
                case answer.none:
                    quizManager.CurrentItem.Answer = string.Empty;
                    break;
            }
            if(quizManager.NextQuestion())
            {
                count = 1;
                restartItem();
                populateItem(quizManager.CurrentItem);
            }
            else
            {

                timer.Stop();
                quizActivity.ReplaceFragment(quizActivity.endQuizFragment);
            }
               

           
        }
        private void NextQuestion()
        {
            quizManager.NextQuestion();
        }
        private void BtnD_Click(object sender, EventArgs e)
        {
            itemAnswer(answer.d);
        }

        private void BtnC_Click(object sender, EventArgs e)
        {
            itemAnswer(answer.c);
        }

        private void BtnB_Click(object sender, EventArgs e)
        {
            itemAnswer(answer.b);
        }

        private void BtnA_Click(object sender, EventArgs e)
        {
            itemAnswer(answer.a);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.fragment_quiz_quizitem, container, false);
            return view;
        }
    }
}