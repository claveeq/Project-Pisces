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

namespace ThesisClient.QuizFragments
{
    public class EndQuizFragment : Android.App.Fragment
    {
        QuizActivity quizActivity;
        QuizManager quizManager;

        Button btnEnqQuiz;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your fragment here
        }
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            quizActivity = Activity as QuizActivity;
            quizManager = quizActivity.quizManager;
            btnEnqQuiz = View.FindViewById<Button>(Resource.Id.fragment_quiz_btnDone);
            btnEnqQuiz.Click += BtnEnqQuiz_Click;
        }

        private void BtnEnqQuiz_Click(object sender, EventArgs e)
        {
            quizManager.SendQuiz();
            quizActivity.Finish();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.fragment_quiz_endQuiz, container, false);
            return view;
        }
    }
}