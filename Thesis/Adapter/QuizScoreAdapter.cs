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
using Thesis.Model;

namespace Thesis.Adapter
{
    class QuizScoreAdapter : BaseAdapter<StudentQuizScore>
    {
        List<StudentQuizScore> quizData;
        Context context;

        public QuizScoreAdapter(Context context, List<StudentQuizScore> QuizData)
        {
            this.context = context;
            quizData = QuizData; 
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;
            QuizScoreAdapterViewHolder holder = null;

            if(view != null)
                holder = view.Tag as QuizScoreAdapterViewHolder;

            if(holder == null)
            {
                holder = new QuizScoreAdapterViewHolder();
                var inflater = context.GetSystemService(Context.LayoutInflaterService).JavaCast<LayoutInflater>();
                //replace with your item and your holder items
                //comment back in
                view = inflater.Inflate(Resource.Layout.item_quizScore, parent, false);
                holder.Name = view.FindViewById<TextView>(Resource.Id.item__quizscore_name);
                holder.Score = view.FindViewById<TextView>(Resource.Id.item__quizscore_score);
                view.Tag = holder;
            }


            //fill in your items
            holder.Name.Text = "Name: " + quizData[position].name;
            holder.Score.Text = "Score: " + quizData[position].score.ToString() + "/"+ quizData[position].items.Count;

            return view;
        }

        //Fill in cound here, currently 0
        public override int Count
        {
            get
            {
                return quizData.Count;
            }
        }

        public override StudentQuizScore this[int position]
        {
            get
            {
                return quizData[position];
            }
        }
    }

    class QuizScoreAdapterViewHolder : Java.Lang.Object
    {
        public TextView Name { get; set; }
        public TextView Score { get; set; }
    }
}