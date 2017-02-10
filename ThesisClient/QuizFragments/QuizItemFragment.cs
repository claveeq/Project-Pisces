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

namespace ThesisClient.QuizFragments
{
    public class QuizItemFragment : Android.App.Fragment
    {
        EditText tvQuestion;
        Button btnA;
        Button btnB;
        Button btnC;
        Button btnD;
        enum answer { a,b,c,d }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            initViews();
        }

        private void initViews()
        {
            tvQuestion = View.FindViewById<EditText>(Resource.Id.fragment_quiz_tvQuestion);
            btnA = View.FindViewById<Button>(Resource.Id.fragment_quiz_btnA);
            btnB = View.FindViewById<Button>(Resource.Id.fragment_quiz_btnB);
            btnC = View.FindViewById<Button>(Resource.Id.fragment_quiz_btnC);
            btnD = View.FindViewById<Button>(Resource.Id.fragment_quiz_btnD);
            btnA.Click += BtnA_Click;
            btnB.Click += BtnB_Click;
            btnC.Click += BtnC_Click;
            btnD.Click += BtnD_Click;
        }
        private void itemAnswer(answer ans)
        {
            switch(ans)
            {
                case answer.a:
                    break;
                case answer.b:
                    break;
                case answer.c:
                    break;
                case answer.d:
                    break;
                default:
                    break;
            }
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