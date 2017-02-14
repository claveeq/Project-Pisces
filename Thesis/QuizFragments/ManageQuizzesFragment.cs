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
using Thesis.Adapter;
using Thesis.Activities;
using Android.Support.V7.Widget;
using Thesis.Helper;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.Design.Widget;

namespace Thesis.QuizFragments
{
    public class ManageQuizzesFragment : Fragment
    {
        EditText etQuizTitle;
       // ListView lvQuizzes;
        Toolbar tbQuizItem;

        RecyclerView rvQuizItems;
        QuizRecyleViewAdapter quizRecycleViewAdapter;
        RecyclerView.LayoutManager layoutManger;

        QuizTitleAdapter quizAdapter;

        CreateQuizActivity quizActivity;
        QuizManager quizManager;
        //List<Quiz> quiz;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your fragment here
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            quizActivity = Activity as CreateQuizActivity;
            quizManager = quizActivity.quizManager;
            etQuizTitle = View.FindViewById<EditText>(Resource.Id.fragment_manageQuizzes_etQuizTitle);
           // lvQuizzes = View.FindViewById<ListView>(Resource.Id.fragment_manageQuizzes_lvQuizzes);
            quizAdapter = new QuizTitleAdapter(quizActivity, quizManager.GetAllQuizzes());
            //lvQuizzes.Adapter = quizAdapter;

            //lvQuizzes.ItemClick += LvQuizzes_ItemClick;
            if(quizManager.Quiz != null)
            {
                etQuizTitle.Text = quizManager.Quiz.Title;
                //recycle view
                rvQuizItems = View.FindViewById<RecyclerView>(Resource.Id.fragment_manageQuizzes_rvQuizItems);
                rvQuizItems.HasFixedSize = true;
                layoutManger = new LinearLayoutManager(quizActivity);
                rvQuizItems.SetLayoutManager(layoutManger);
                quizRecycleViewAdapter = new QuizRecyleViewAdapter(quizManager.Quiz.GetQuizitems);
                rvQuizItems.SetAdapter(quizRecycleViewAdapter);
                quizRecycleViewAdapter.ItemClick += QuizRecycleViewAdapter_ItemClick;
            }
            
            //Toolbar
            tbQuizItem = View.FindViewById<Toolbar>(Resource.Id.fragment_manageQuizzes_tbQuizItems);
            tbQuizItem.InflateMenu(Resource.Menu.quizitem_tools_menu);
            tbQuizItem.MenuItemClick += TbQuiz_MenuItemClick;
        }

        private void TbQuiz_MenuItemClick(object sender, Toolbar.MenuItemClickEventArgs e)
        {
            switch(e.Item.ItemId)
            {
 
                case (Resource.Id.nav_add)://CREATE NEW QUIZ
                    //if(quizManager.Quiz != null)
                    //    quizManager.currentItemNo = quizManager.Quiz.GetQuizitems.Count + 1;
                    quizManager.CreateQuiz(etQuizTitle.Text);
                    quizActivity.ReplaceFragment(quizActivity.questionItemFragment);
                    break;
                case (Resource.Id.nav_save)://DELETE A QUIZ
                    var builder = new Android.Support.V7.App.AlertDialog.Builder(Activity);
                    builder.SetTitle("Save");
                    builder.SetMessage("Sure you want to modify and save " + quizManager.Quiz.Title + "?");
                    builder.SetPositiveButton("Save", (senderAlert, args) =>
                    {
                        quizManager.SerializeQuiz();
                        Snackbar.Make(View, "Quiz modified :)", Snackbar.LengthShort).Show();
                        quizActivity.Finish();
                    });
                    builder.SetNegativeButton("Cancel", (senderAlert, args) => { });

                    Dialog dialog = builder.Create();
                    dialog.Show();
                    break;
            }
        }

        private void QuizRecycleViewAdapter_ItemClick(object sender, QuizRecyleViewAdapterClickEventArgs e)
        {
            int position = e.Position + 1;
            quizManager.currentItemNo = position;
            quizActivity.ReplaceFragment(quizActivity.questionItemFragment);
        }

        //private void LvQuizzes_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        //{
        //    quizManager.DeserializeQuiz(quizAdapter.GetQuizName(e.Position));
        //    quizActivity.ReplaceFragment(quizActivity.questionItemFragment);
        //}

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
           View view =  inflater.Inflate(Resource.Layout.fragment_quiz_manageQuizzes, container, false);
            return view;
        }
    }
}