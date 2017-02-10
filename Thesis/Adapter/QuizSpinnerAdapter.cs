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

namespace Thesis.Adapter
{
    class QuizSpinnerAdapter : BaseAdapter
    {

        List<string> quizzes;
        Context context;
        public QuizSpinnerAdapter(Context context, List<string> quizzes)
        {
            this.context = context;
            this.quizzes = quizzes;
        }


        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }
        public string GetQuizName(int position)
        {
            return quizzes[position].ToString();
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;
            QuizSpinnerAdapterViewHolder holder = null;

            if(view != null)
                holder = view.Tag as QuizSpinnerAdapterViewHolder;

            if(holder == null)
            {
                holder = new QuizSpinnerAdapterViewHolder();
                var inflater = context.GetSystemService(Context.LayoutInflaterService).JavaCast<LayoutInflater>();
                //replace with your item and your holder items
                //comment back in
                view = inflater.Inflate(Resource.Layout.item_quiz, parent, false);
                holder.Title = view.FindViewById<TextView>(Resource.Id.item_quiz_title);
                view.Tag = holder;
            }


            //fill in your items
            holder.Title.Text = quizzes[position].ToString();
            //holder.Subject.Text = quizzes[position].GetTeachersID.ToString();
            //holder.Subject.Text = quizzes[position].ID.ToString();
            //holder.Title.Text = "new text here";

            return view;
        }

        //Fill in cound here, currently 0
        public override int Count
        {
            get
            {
                return quizzes.Count;
            }
        }

    }

    class QuizSpinnerAdapterViewHolder : Java.Lang.Object
    {
        //Your adapter views to re-use
        public TextView Title { get; set; }
    }
}