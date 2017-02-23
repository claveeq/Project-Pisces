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
using Android.Support.Design.Widget;

namespace Thesis.Fragments
{
    public class ActiveHomeFragment : Fragment
    {
        ClassroomManager classManager;
        DashboardActivity dashActivity;
        Button btnEnd;
        Button btnStartQuiz;
        Button btnOpenQuizScores;
        Spinner spQuizzes;
        Spinner spLectures;
        QuizSpinnerAdapter quizSpinnerAdapter;
        LecturesAdapter lecturesAdapter;
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
            quizManager = dashActivity.quizManager;

              btnStartQuiz = View.FindViewById<Button>(Resource.Id.fragment_home_active_btnStartQuiz);
            btnEnd = View.FindViewById<Button>(Resource.Id.fragment_home_active_btnEndClass);
            btnOpenQuizScores = View.FindViewById<Button>(Resource.Id.fragment_home_active_btnOpenQuizScoresFolder);
            spQuizzes = View.FindViewById<Spinner>(Resource.Id.fragment_home_active_spQuizzes);
            spLectures = View.FindViewById<Spinner>(Resource.Id.fragment_home_active_spLectures);

            quizSpinnerAdapter = new QuizSpinnerAdapter(dashActivity, quizManager.GetAllQuizzes());
            spQuizzes.Adapter = quizSpinnerAdapter;

            lecturesAdapter = new LecturesAdapter(dashActivity, classManager.GetLecturesFileNames());
            spLectures.Adapter = lecturesAdapter;

            btnStartQuiz.Click += BtnStartQuiz_Click;
            btnEnd.Click += Buttond_Click;
            btnOpenQuizScores.Click += BtnOpenQuizScores_Click;
            spQuizzes.ItemSelected += SpQuizzes_ItemSelected;
        }

        private void BtnOpenQuizScores_Click(object sender, EventArgs e)
        {
            quizManager.OpenQuizScoresFolder(Activity);
        }

        private void SpQuizzes_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var teachersSubject =  quizManager.GetTeachersSubjects();
            quizName = quizSpinnerAdapter.GetQuizName(e.Position);
        }

        private void BtnStartQuiz_Click(object sender, EventArgs e)
        {
            if(quizName == string.Empty || quizName == null)
            {
                Snackbar.Make(btnStartQuiz, "Choose a Quiz", Snackbar.LengthShort).Show();
                return;
            }
            if(classManager.ClassroomIsActive)
            {
                quizManager.DeserializeQuiz(quizName);
                quizManager.StartQuiz();
                classManager.QuizIsActive = true;
                dashActivity.ReplaceFragment(dashActivity.activeQuizFragment);
            }
        }

        private void Buttond_Click(object sender, EventArgs e)
        {

            classManager.SaveAttendanceToCSV();
            classManager.ClassroomIsActive = false;
            ServerController.CloseAllSockets();
            dashActivity.ReplaceFragment(dashActivity.homeFragment);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            return inflater.Inflate(Resource.Layout.fragment_home_active, container, false);
        }
    }
}