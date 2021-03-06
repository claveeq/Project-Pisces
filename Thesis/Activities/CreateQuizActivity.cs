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
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using Thesis.QuizFragments;
using Android.Support.Design.Widget;

namespace Thesis.Activities
{
    [Activity(Label = "CreateQuizActivity")]
    public class CreateQuizActivity : Activity
    {
        //Fragment
        public QuestionItemFragment questionItemFragment;
        public ManageQuizzesFragment manageQuizFragment;
        FragmentTransaction trans;
        Stack<Fragment> stackFragments;
        Fragment currentFragment = new Fragment();
        bool IsToManage;
        public QuizManager quizManager;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CreateQuiz);
            int teachersID = Intent.GetIntExtra("teachersID", 0);
            string quizTitle = Intent.GetStringExtra("quizTitle");
            IsToManage = Intent.GetBooleanExtra("manage", false);
           
            fragmentInstantiators();

            quizManager = new QuizManager(teachersID);
            if(quizTitle != null)
                quizManager.DeserializeQuiz(quizTitle);
        }

        private void fragmentInstantiators()
        {
            questionItemFragment = new QuestionItemFragment();
            manageQuizFragment = new ManageQuizzesFragment();
            stackFragments = new Stack<Fragment>();
  
            trans = FragmentManager.BeginTransaction();
            ////if(IsToManage)//if true, another fragment will show up for editing previous quizzes
            ////{
            //trans.Add(Resource.Id.frame_createQuiz_frame, questionItemFragment, "itemQuiz");
            //trans.Hide(questionItemFragment);
            trans.Add(Resource.Id.frame_createQuiz_frame, manageQuizFragment, "manageQuiz");
                currentFragment = manageQuizFragment;
            //}
            //else
            //{ 
            //    trans.Add(Resource.Id.frame_createQuiz_frame, quizInfoFragment, "quizinfo");
            //    currentFragment = quizInfoFragment;
            //}
            trans.Commit();
        }

        public void ReplaceFragment(Fragment fragment)
        {
            if(fragment.IsVisible)
            {
                return;
            }

            var trans = FragmentManager.BeginTransaction();
            trans.Replace(Resource.Id.frame_createQuiz_frame, fragment);
            trans.AddToBackStack(null);
            trans.Commit();

            currentFragment = fragment;

        }
        public void ShowFragment(Fragment fragment)
        {
            if(fragment.IsVisible)
            {
                return;
            }

            var fragmentTx = FragmentManager.BeginTransaction();

            fragment.View.BringToFront();
            currentFragment.View.BringToFront();

            fragmentTx.Hide(currentFragment);
            fragmentTx.Show(fragment);

            fragmentTx.AddToBackStack(null);
            stackFragments.Push(currentFragment);
            fragmentTx.Commit();

            currentFragment = fragment;
        }
        public override void OnBackPressed()
        {          
            //if(FragmentManager.BackStackEntryCount > 0)
            //{
            //    FragmentManager.PopBackStack();
            //    currentFragment = stackFragments.Pop();
            //}
            //else
            //{
            var builder = new Android.Support.V7.App.AlertDialog.Builder(this);
            builder.SetTitle("Exit");
            builder.SetMessage("Sure you want to exit? Any changes will be voided.");
            builder.SetPositiveButton("Yes", (senderAlert, args) => {
                Finish();
            });
            builder.SetNegativeButton("No", (senderAlert, args) => {
                Snackbar.Make(FindViewById(Resource.Id.frame_createQuiz_frame), "Then, let's Work!", Snackbar.LengthShort).Show();
            });
            Dialog dialog = builder.Create();
            dialog.Show();
            //base.OnBackPressed();
        }
    }
}