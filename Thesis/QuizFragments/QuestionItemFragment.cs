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
using Android.Support.Design.Widget;

namespace Thesis.QuizFragments
{
    public class QuestionItemFragment : Fragment
    {

        EditText etQuestion;
        EditText etA;
        EditText etB;
        EditText etC;
        EditText etD;
        TextView tvNo;
        RadioGroup rgChoices;
        Button btnNext;
        Button btnPrevious;
        Button btnEnd;
        Button btnDelete;
        CreateQuizActivity quizActivity;
        QuizManager quizManager;
        RadioButton rbChoices;
        string correctAnswer;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
      
            // Create your fragment here
        }

    
        private void initViews()
        {
            tvNo = View.FindViewById<TextView>(Resource.Id.fragment_questionItem_tvItemNo);
            etQuestion = View.FindViewById<EditText>(Resource.Id.fragment_questionItem_etQuestion);
            etA = View.FindViewById<EditText>(Resource.Id.fragment_questionItem_etA);
            etB = View.FindViewById<EditText>(Resource.Id.fragment_questionItem_etB);
            etC = View.FindViewById<EditText>(Resource.Id.fragment_questionItem_etC);
            etD = View.FindViewById<EditText>(Resource.Id.fragment_questionItem_etD);
            btnNext = View.FindViewById<Button>(Resource.Id.fragment_questionItem_btnNext);
            btnPrevious = View.FindViewById<Button>(Resource.Id.fragment_questionItem_btnPrevious);
            btnEnd = View.FindViewById<Button>(Resource.Id.fragment_questionItem_btnEnd);
            btnDelete = View.FindViewById<Button>(Resource.Id.fragment_questionItem_btnDelete);
            rgChoices = View.FindViewById<RadioGroup>(Resource.Id.fragment_questionItem_rgChoices);

        }

        private void ClearItems()
        {
            string no = quizManager.currentItemNo + @"/" + quizManager.GetQuizItems.Count; 
            tvNo.Text = no;
            etQuestion.Text = string.Empty;
            etA.Text = string.Empty;
            etB.Text = string.Empty;
            etC.Text = string.Empty;
            etD.Text = string.Empty;
            rgChoices.ClearCheck();

        }
        private void PopulateViews(QuizItem quiz)
        {
            if(quiz == null)
                return;

            etQuestion.Text = quiz.Question;
            etA.Text = quiz.AnsA;
            etB.Text = quiz.AnsB;
            etC.Text = quiz.AnsC;
            etD.Text = quiz.AnsD;
        
            if(quiz.Answer == quiz.AnsA)
                rbChoices = View.FindViewById<RadioButton>(Resource.Id.fragment_questionItem_rbA);   
            else if(quiz.Answer == quiz.AnsB)
                rbChoices = View.FindViewById<RadioButton>(Resource.Id.fragment_questionItem_rbB);
            else if(quiz.Answer == quiz.AnsC)
                rbChoices = View.FindViewById<RadioButton>(Resource.Id.fragment_questionItem_rbC);
            else if(quiz.Answer == quiz.AnsD)
                rbChoices = View.FindViewById<RadioButton>(Resource.Id.fragment_questionItem_rbD);
            rbChoices.Checked = true;

            string no = quiz.ItemNo + @"/" + quizManager.GetQuizItems.Count;
            tvNo.Text = no;

        }
        private void UpdateQuizItem(QuizItem quiz)
        {
            if(quiz == null)
                return;

            quiz.Question = etQuestion.Text;
            quiz.AnsA = etA.Text;
            quiz.AnsB = etB.Text;
            quiz.AnsC = etC.Text;
            quiz.AnsD = etD.Text;
            quiz.Answer = correctAnswer;

        }
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            initViews();

            quizActivity = Activity as CreateQuizActivity;
            quizManager = quizActivity.quizManager;
            btnNext.Click += BtnNext_Click;
            btnEnd.Click += BtnEnd_Click;
            btnPrevious.Click += BtnPrevious_Click;
            btnDelete.Click += BtnDelete_Click;
            ClearItems();
            if(quizManager.Quiz != null)//if managing quiz
                PopulateViews(quizManager.itemNavigation(quizitemNavigation.previous));

        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            quizManager.DeleteItem(quizManager.currentItemNo);
            ClearItems();
            PopulateViews(quizManager.itemNavigation(quizitemNavigation.delete));
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            if(etQuestion.Text == string.Empty)
            {
                Snackbar.Make(btnNext, "Remember to write a Question.", Snackbar.LengthShort).Show();
                return;
            }
            if(!radioButtonSelected())
            {
                Snackbar.Make(btnNext, "Choose an answer!", Snackbar.LengthShort).Show();
                return;
            }
            var quizCount = quizManager.GetQuizItems.Count;//total count of quizitems
            if(quizCount == 0 || quizCount < quizManager.currentItemNo)
            {
                quizManager.AddItem(etQuestion.Text, etA.Text, etB.Text, etC.Text, etD.Text, correctAnswer);
                quizManager.itemNavigation(quizitemNavigation.add);
                ClearItems();
            }
            else
            {
                var item = quizManager.GetQuizItems.Where(x => x.ItemNo == quizManager.currentItemNo).FirstOrDefault();
                UpdateQuizItem(item);
                ClearItems();
                var quizitem = quizManager.itemNavigation(quizitemNavigation.next);
                PopulateViews(quizitem);
            }
            string no = quizManager.currentItemNo + @"/" + quizManager.GetQuizItems.Count;
            tvNo.Text = no;
            
        }
        
        private void BtnPrevious_Click(object sender, EventArgs e)
        {
            ClearItems();
            PopulateViews(quizManager.itemNavigation(quizitemNavigation.previous));
        }

        private void BtnEnd_Click(object sender, EventArgs e)
        {
            quizActivity.ReplaceFragment(quizActivity.finilizeQuizFragment);
        }
        private bool radioButtonSelected()
        {
            RadioButton checkedRadioButton = View.FindViewById<RadioButton>(rgChoices.CheckedRadioButtonId);
            try
            {
                if(checkedRadioButton.Id == Resource.Id.fragment_questionItem_rbA)
                {
                    correctAnswer = etA.Text;
                    return true;
                }
                else if(checkedRadioButton.Id == Resource.Id.fragment_questionItem_rbB)
                {
                    correctAnswer = etB.Text;
                    return true;
                }
                else if(checkedRadioButton.Id == Resource.Id.fragment_questionItem_rbC)
                {
                    correctAnswer = etC.Text;
                    return true;
                }
                else if(checkedRadioButton.Id == Resource.Id.fragment_questionItem_rbD)
                {
                    correctAnswer = etD.Text;
                    return true;
                }
                else
                    return false;
            }
            catch(Exception)
            {
                return false;
            }        
        }
 

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View view =  inflater.Inflate(Resource.Layout.fragment_quiz_questionItem, container, false);
            return view;
        }
    }
}