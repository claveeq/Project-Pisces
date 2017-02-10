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
using ThesisClient.Activities;

namespace ThesisClient.QuizFragments
{
    public class QuizInfoFragment : Android.App.Fragment
    {
        QuizActivity quizActivity;

        Button btnStart;
        TextView tvTitle;
        TextView tvDescription;
        TextView tvNoItems;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your fragment here
        }
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            quizActivity = Activity as QuizActivity;
            btnStart = View.FindViewById<Button>(Resource.Id.fragment_quizinfo_btnStart);
            tvTitle = View.FindViewById<TextView>(Resource.Id.fragment_quizinfo_tvTitle);
            tvDescription = View.FindViewById<TextView>(Resource.Id.fragment_quizinfo_tvTitle);
            tvNoItems = View.FindViewById<TextView>(Resource.Id.fragment_quizinfo_tvTitle);

            btnStart.Click += BtnStart_Click;
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            quizActivity.ReplaceFragment(quizActivity.quizItemFragment);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View view =   inflater.Inflate(Resource.Layout.fragment_quiz_quizinfo, container, false);
            return view;
        }
    }
}