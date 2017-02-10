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
using Thesis.Adapter;
using Thesis.Activities;

namespace Thesis.QuizFragments
{
    public class ManageQuizzesFragment : Fragment
    {
        ListView lvQuizzes;
        QuizAdapter quizAdapter;

        CreateQuizActivity quizActivity;
        QuizManager quizManager;
        //List<Quiz> quiz;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your fragment here
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            quizActivity = Activity as CreateQuizActivity;
            quizManager = quizActivity.quizManager;
            lvQuizzes = View.FindViewById<ListView>(Resource.Id.fragment_manageQuizzes_lvQuizzes);
            quizAdapter = new QuizAdapter(quizActivity, quizManager.GetAllQuizzes());
            lvQuizzes.Adapter = quizAdapter;

            lvQuizzes.ItemClick += LvQuizzes_ItemClick;
        }

        private void LvQuizzes_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            quizManager.DeserializeQuiz(quizAdapter.GetQuizName(e.Position));
            quizActivity.ReplaceFragment(quizActivity.quizInfoFragment);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
           View view =  inflater.Inflate(Resource.Layout.fragment_quiz_manageQuizzes, container, false);
            return view;
        }
    }
}