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
using Thesis;
using Thesis.Adapter;
using Android.Support.Design.Widget;

namespace Thesis.QuizFragments
{
    public class QuizInfoFragment : Fragment
    {
        EditText etTitle;
        Spinner spSubjects;
        Button btnNext;
        SubjectSpinnerAdapter subjectSpinnerAdapter; 
        CreateQuizActivity quizActivity;
        QuizManager quizManager;
        Subject selectedSubject;
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
            subjectSpinnerAdapter = new SubjectSpinnerAdapter(quizActivity,quizManager.GetTeachersSubjects());
            spSubjects.Adapter = subjectSpinnerAdapter;
            if(quizManager.Quiz != null)
            {
                etTitle.Text = quizManager.Quiz.Title;
                int position =  quizManager.GetTeachersSubjects().FindIndex(x => x.GetTitle == quizManager.Quiz.Subject);
                spSubjects.SetSelection(position);
            }
            btnNext.Click += BtnNext_Click;
            spSubjects.ItemSelected += SpSubjects_ItemSelected;
        }
        
        private void SpSubjects_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var teachersSubject = quizManager.GetTeachersSubjects();
            selectedSubject = teachersSubject[e.Position];
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            if(selectedSubject == null)
            {
                Snackbar.Make(btnNext, "Choose a Subject.", Snackbar.LengthShort).Show();
                return;
            }
            if(etTitle.Text == string.Empty)
            {
                Snackbar.Make(btnNext, "Title, Please:)", Snackbar.LengthShort).Show();
                return;
            }
            if(quizManager.Quiz == null)
                quizManager.CreateQuiz(etTitle.Text, selectedSubject.GetTitle);
            else
            {
                quizManager.Quiz.Title = etTitle.Text;
                quizManager.Quiz.Subject = selectedSubject.GetTitle;
            }


            quizActivity.ReplaceFragment(quizActivity.questionItemFragment);
        }

        private void initViews()
        {
            etTitle = View.FindViewById<EditText>(Resource.Id.fragment_quizinfo_etTitle);
            spSubjects = View.FindViewById<Spinner>(Resource.Id.fragment_quizinfo_spSubjects);
            btnNext = View.FindViewById<Button>(Resource.Id.fragment_quizInfo_btnNext);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.fragment_quiz_quizInfo, container, false);
            // Use this to return your custom view for this Fragment
            return view;
        }
    }
}