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
using Thesis.Activities;
using Thesis.Adapter;

namespace Thesis.Fragments
{
    public class ActiveQuizFragment : Fragment
    {
        DashboardActivity dashActivity;
        ClassroomManager classManager;
        QuizManager quizManager;
        Button btnExportScores;
        Button btnEndQuiz;
        ListView lvTuizScoreItems;
        QuizScoreAdapter quizScoreAdapter;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your fragment here
        }
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            dashActivity = Activity as DashboardActivity;
            classManager = dashActivity.GetClassManager;
            quizManager = dashActivity.quizManager;

            btnExportScores = View.FindViewById<Button>(Resource.Id.fragment_activequiz_btnExportScores);
            btnEndQuiz = View.FindViewById<Button>(Resource.Id.fragment_activequiz_btnEndQuiz);
            lvTuizScoreItems = View.FindViewById<ListView>(Resource.Id.fragment_activequiz_lvTuizScoreItems);
            quizScoreAdapter = new QuizScoreAdapter(dashActivity, quizManager.CheckQuiz());
            quizScoreAdapter.NotifyDataSetChanged();
            lvTuizScoreItems.Adapter = quizScoreAdapter;

            btnExportScores.Click += BtnExportScores_Click;
            btnEndQuiz.Click += BtnEndQuiz_Click;
        }
        private void BtnEndQuiz_Click(object sender, EventArgs e)
        {
            quizManager.EndQuiz();
            quizManager.SaveQuizToCSV(quizManager.CheckQuiz());
       //     ServerController.QuizDoneItems = null; 
            classManager.QuizIsActive = false;
            dashActivity.ReplaceFragment(dashActivity.activeHomeFragment);
        }

        private void BtnExportScores_Click(object sender, EventArgs e)
        {
            quizManager.CheckQuiz();
            quizScoreAdapter.NotifyDataSetChanged();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.fragment_active_quiz, container, false);

            return view;
        }
    }
}