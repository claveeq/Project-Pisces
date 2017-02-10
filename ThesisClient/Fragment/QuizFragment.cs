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
namespace ThesisClient.Fragment
{
    public class QuizFragment : Android.App.Fragment
    {
        Intent intent;
        Button btnTakeQuiz;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            btnTakeQuiz = View.FindViewById<Button>(Resource.Id.fragment_quiz_btnTakeQuiz);
            btnTakeQuiz.Click += BtnTakeQuiz_Click;

        }

        private void BtnTakeQuiz_Click(object sender, EventArgs e)
        {
            // intent = new Intent(Activity, typeof(QuizActivity));
            // StartActivity(intent);
            ClientController.SendRequest(Task.quiz);

            
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.fragment_quiz, container, false);
            return view;

        }
    }
}