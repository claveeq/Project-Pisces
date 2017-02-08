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
        public QuizInfoFragment quizInfoFragment;
        public QuestionItemFragment questionItemFragment;
        public FinilizeQuizFragment finilizeQuizFragment;
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
            IsToManage = Intent.GetBooleanExtra("manage", false);
            fragmentInstantiators();

            quizManager = new QuizManager(teachersID);
        }

        private void fragmentInstantiators()
        {
            quizInfoFragment = new QuizInfoFragment();
            questionItemFragment = new QuestionItemFragment();
            finilizeQuizFragment = new FinilizeQuizFragment();
            manageQuizFragment = new ManageQuizzesFragment();
            stackFragments = new Stack<Fragment>();
  
            trans = FragmentManager.BeginTransaction();
            if(IsToManage)//if true, another fragment will show up for editing previous quizzes
            {
                trans.Add(Resource.Id.frame_createQuiz_frame, manageQuizFragment, "manageQuiz");
                currentFragment = manageQuizFragment;
            }
            else
            { 
                trans.Add(Resource.Id.frame_createQuiz_frame, quizInfoFragment, "quizinfo");
                currentFragment = quizInfoFragment;
            }
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
            builder.SetTitle("Confirm delete");
            builder.SetMessage("Are you sure you want to exit? The quiz will be voided.");
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
        //}
    }
}