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
    public class ActiveHomeFragment : Fragment
    {
        ClassroomManager classManager;
        DashboardActivity dashActivity;
        Button btnEnd;
        Button btnStartQuiz;
        Spinner spQuizzes;
        QuizSpinnerAdapter quizSpinnerAdapter;
        QuizManager quizManager;
        string quizName;
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
            quizManager = new QuizManager(classManager.GetTeacher.GetID);

            btnStartQuiz = View.FindViewById<Button>(Resource.Id.fragment_home_active_btnStartQuiz);
            btnEnd = View.FindViewById<Button>(Resource.Id.fragment_home_active_btnEndClass);
            spQuizzes = View.FindViewById<Spinner>(Resource.Id.fragment_home_active_spQuizzes);
            quizSpinnerAdapter = new QuizSpinnerAdapter(dashActivity, quizManager.GetAllQuizzes());
            spQuizzes.Adapter = quizSpinnerAdapter;

            btnStartQuiz.Click += BtnStartQuiz_Click;
            btnEnd.Click += Buttond_Click;
            spQuizzes.ItemSelected += SpQuizzes_ItemSelected;
        }

        private void SpQuizzes_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var teachersSubject = quizManager.GetTeachersSubjects();
            quizName = quizSpinnerAdapter.GetQuizName(e.Position);
        }

        private void BtnStartQuiz_Click(object sender, EventArgs e)
        {
            if(classManager.ClassroomIsActive)
            {
                quizManager.DeserializeQuiz(quizName);
                quizManager.StartQuiz();
           
            }
        }

        private void Buttond_Click(object sender, EventArgs e)
        {
            classManager.SaveAttendanceToCSV();
            classManager.ClassroomIsActive = false;
            ServerController.CloseAllSockets();
            dashActivity.ReplaceFragment(dashActivity.homeFragment);
            Toast.MakeText(dashActivity, classManager.activateIsLate.ToString(), ToastLength.Short).Show();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            return inflater.Inflate(Resource.Layout.fragment_home_active, container, false);
        }
    }
}