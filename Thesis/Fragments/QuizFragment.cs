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

namespace Thesis.Fragments
{
    public class QuizFragment : Fragment
    {
        Button btnCreateQuiz;
        Button btnManageQuizzes;
        ListView lvQuizzes;
        Toolbar tbQuiz;

        QuizAdapter quizAdapter;
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
            quizAdapter = new QuizAdapter(dashActivity, quizManager.GetAllQuizzes());

            btnCreateQuiz = View.FindViewById<Button>(Resource.Id.fragment_quiz_btnCreateQuiz);
            tbQuiz = View.FindViewById<Toolbar>(Resource.Id.fragment_quiz_tbQuiz);
            //btnManageQuizzes = View.FindViewById<Button>(Resource.Id.fragment_quiz_btnManageQuizzes);
            lvQuizzes = View.FindViewById<ListView>(Resource.Id.fragment_quiz_lvQuizzes);
            lvQuizzes.Adapter = quizAdapter;
            lvQuizzes.ItemClick += LvQuizzes_ItemClick;

            tbQuiz.InflateMenu(Resource.Menu.quiz_tools_menu);
            tbQuiz.MenuItemClick += TbQuiz_MenuItemClick;
   
            btnCreateQuiz.Click += delegate
            {
              
            };
        }

        private void LvQuizzes_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            quizName = quizManager.GetAllQuizzes()[e.Position];
            quizAdapter.selectedPosition(e.Position);
            quizAdapter.NotifyDataSetChanged();
        }

        private void TbQuiz_MenuItemClick(object sender, Toolbar.MenuItemClickEventArgs e)
        {
            //react to click here and swap fragments or navigate
            switch(e.Item.ItemId)
            {
                case (Resource.Id.nav_toggle):
                    //toggle student if he/she is in the class or not
                    if(classManager.CurrentSubject.ID == 0)
                    {
                        //Snackbar.Make(View, "Sorry. Can't do that here, remember:)", Snackbar.LengthShort).Show();
                        return;
                    }
                    //selectedStudent.toggleInThisSubject();
                    //studentAdapter.NotifyDataSetChanged();
                    break;
                case (Resource.Id.nav_add):
                    //navigate to add fragment
                    intent = new Intent(dashActivity, typeof(CreateQuizActivity));
                    intent.PutExtra("teachersID", classManager.GetTeacher.GetID);
                    StartActivity(intent);
                    break;
                case (Resource.Id.nav_delete):
                    //var builder = new AlertDialog.Builder(Activity);
                    //builder.SetTitle("Confirm delete");
                    ////builder.SetMessage("Sure you want to delete " + selectedStudent.GetFirstName + "?");
                    //builder.SetPositiveButton("Delete", (senderAlert, args) =>
                    //{
                    //    classManager.DeleteStudent(selectedStudent);
                    //    studentAdapter.NotifyDataSetChanged();
                    //    Snackbar.Make(View, "Student Deleted", Snackbar.LengthShort).Show();
                    //});
                    //builder.SetNegativeButton("Cancel", (senderAlert, args) =>
                    //{
                    //    Snackbar.Make(View, "Canceled", Snackbar.LengthShort).Show();
                    //});

                    //Dialog dialog = builder.Create();
                    //dialog.Show();
                    break;
                case (Resource.Id.nav_edit):
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