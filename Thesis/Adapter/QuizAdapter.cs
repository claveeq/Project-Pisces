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
    class QuizAdapter : BaseAdapter<string>
    {
        List<string> quizzes;
        Context context;
        public QuizAdapter(Context context, List<string> quizzes)
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
            QuizzesAdapterViewHolder holder = null;

            if(view != null)
                holder = view.Tag as QuizzesAdapterViewHolder;

            if(holder == null)
            {
                holder = new QuizzesAdapterViewHolder();
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

        public override string this[int position]
        {
            get
            {
                return quizzes[position];
            }
        }

        class QuizzesAdapterViewHolder : Java.Lang.Object
        {
            //Your adapter views to re-use
            public TextView Title { get; set; }
        }
    }
}