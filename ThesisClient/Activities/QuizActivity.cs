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
using ThesisClient.QuizFragments;

namespace ThesisClient.Activities
{
    [Activity(Label = "QuizActivity")]
    public class QuizActivity : Activity
    {
        public FragmentTransaction trans;
        public Stack<Android.App.Fragment> stackFragments;
        public Android.App.Fragment currentFragment = new Android.App.Fragment();
        //Fragments
        public QuizInfoFragment quizInfoFragment;
        public QuizItemFragment quizItemFragment;
        public EndQuizFragment endQuizFragment;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Quiz);
            initViews();
            // Create your application here
        }

        private void initViews()
        {
            quizInfoFragment = new QuizInfoFragment();
            quizItemFragment = new QuizItemFragment();
            endQuizFragment = new EndQuizFragment();

            trans = FragmentManager.BeginTransaction();
            trans.Add(Resource.Id.quiz_flQuizContainer, quizInfoFragment, "quizInfo");
            currentFragment = quizInfoFragment;
            trans.Commit();

            stackFragments = new Stack<Android.App.Fragment>();
        }

        public void ReplaceFragment(Android.App.Fragment fragment)
        {
            if(fragment.IsVisible)
                return;
            var trans = FragmentManager.BeginTransaction();
            trans.Replace(Resource.Id.fragmentContainer, fragment);
            trans.AddToBackStack(null);
            trans.Commit();
            currentFragment = fragment;
        }
    }
}