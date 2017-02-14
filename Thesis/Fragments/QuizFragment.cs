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
using Android.Support.V7.App;
using Thesis.Adapter;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.Design.Widget;

namespace Thesis.Fragments
{
    public class QuizFragment : Fragment
    {
        ListView lvQuizzes;
        Toolbar tbQuiz;

        QuizTitleAdapter quizAdapter;
        ClassroomManager classManager;
        DashboardActivity dashActivity;
        Intent intent;
        QuizManager quizManager;

        string quizName;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            dashActivity = Activity as DashboardActivity;
            classManager = dashActivity.GetClassManager;
            quizManager = new QuizManager(classManager.GetTeacher.GetID);
            quizAdapter = new QuizTitleAdapter(dashActivity, quizManager.GetAllQuizzes());

            tbQuiz = View.FindViewById<Toolbar>(Resource.Id.fragment_quiz_tbQuiz);
            lvQuizzes = View.FindViewById<ListView>(Resource.Id.fragment_quiz_lvQuizzes);
            lvQuizzes.Adapter = quizAdapter;
            lvQuizzes.ItemClick += LvQuizzes_ItemClick;

            tbQuiz.InflateMenu(Resource.Menu.quiz_tools_menu);
            tbQuiz.MenuItemClick += TbQuiz_MenuItemClick;
   
        }

        private void LvQuizzes_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
          //  quizAdapter.RefreshList(quizManager.GetAllQuizzes());
            quizName = quizManager.GetAllQuizzes()[e.Position];
            quizAdapter.selectedPosition(e.Position);
            quizAdapter.NotifyDataSetChanged();
        }

        private void TbQuiz_MenuItemClick(object sender, Toolbar.MenuItemClickEventArgs e)
        {
            //react to click here and swap fragments or navigate
            switch(e.Item.ItemId)
            {
                case (Resource.Id.nav_refresh)://CREATE NEW QUIZ
                    quizAdapter.RefreshList(quizManager.GetAllQuizzes());
                    quizAdapter.NotifyDataSetChanged();
                    break;
                case (Resource.Id.nav_openFolder)://CREATE NEW QUIZ

                    break;
                case (Resource.Id.nav_add)://CREATE NEW QUIZ
                    //navigate to add fragment
                    intent = new Intent(dashActivity, typeof(CreateQuizActivity));
                    intent.PutExtra("teachersID", classManager.GetTeacher.GetID);
                    StartActivity(intent);
                    break;
                case (Resource.Id.nav_delete)://DELETE A QUIZ
                    if(quizName == null)
                    {
                        Snackbar.Make(View, "Choose a Quiz you want to delete.", Snackbar.LengthShort).Show();
                        return;
                    }
                    var builder = new Android.Support.V7.App.AlertDialog.Builder(Activity);
                    builder.SetTitle("Confirm delete");
                    builder.SetMessage("Are you sure you want to delete " + quizName  + " quiz?");
                    builder.SetPositiveButton("Delete", (senderAlert, args) =>
                    {
                        quizManager.DeleteQuiz(quizName);
                        quizAdapter.RefreshList(quizManager.GetAllQuizzes());
                        quizAdapter.NotifyDataSetChanged();
                        Snackbar.Make(View, "Quiz Deleted", Snackbar.LengthShort).Show();
                    });
                    builder.SetNegativeButton("Cancel", (senderAlert, args) =>
                    {
                        Snackbar.Make(View, "Canceled", Snackbar.LengthShort).Show();
                    });

                    Dialog dialog = builder.Create();
                    dialog.Show();
                    break;
                case (Resource.Id.nav_edit)://MODIFY A QUIZ
                    if(quizName == null)
                    {
                        Snackbar.Make(View, "Choose a Quiz you want to modify.", Snackbar.LengthShort).Show();
                        return;
                    }
                    intent = new Intent(dashActivity, typeof(CreateQuizActivity));
                    intent.PutExtra("teachersID", classManager.GetTeacher.GetID);
                    intent.PutExtra("quizTitle", quizName);
                    intent.PutExtra("manage", true);
                    StartActivity(intent);
                    break;
            }
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            View view = inflater.Inflate(Resource.Layout.fragment_quiz, container, false);
            return view;
        }
    }
}