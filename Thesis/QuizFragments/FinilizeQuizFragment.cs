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
using Thesis;
using Thesis.Activities;

namespace Thesis.QuizFragments
{
    public class FinilizeQuizFragment : Fragment
    {
        Button btnDone;

        CreateQuizActivity quizActivity;
        QuizManager quizManager;


        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your fragment here
        }
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            initViews();
            quizActivity = Activity as CreateQuizActivity;
            quizManager = quizActivity.quizManager;

        }

        private void initViews()
        {
            btnDone = View.FindViewById<Button>(Resource.Id.fragment_finilizeQuiz_btnDone);

            btnDone.Click += BtnDone_Click;
        }

        private void BtnDone_Click(object sender, EventArgs e)
        {
            quizManager.SerializeQuiz();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.fragment_quiz_finilizeQuiz, container, false);
            return view;
        }
    }
}