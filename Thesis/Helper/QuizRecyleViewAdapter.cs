using System;

using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using System.Collections.Generic;

namespace Thesis.Helper
{
    class QuizRecyleViewAdapter : RecyclerView.Adapter
    {
        public event EventHandler<QuizRecyleViewAdapterClickEventArgs> ItemClick;
        public event EventHandler<QuizRecyleViewAdapterClickEventArgs> ItemLongClick;
       // string[] items;
        List<QuizItem> items = new List<QuizItem>();
        public QuizRecyleViewAdapter(List<QuizItem> data)
        {
            items = data;
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            //Setup your layout here
            View itemView = null;
            var id = Resource.Layout.item_quizItem;
            itemView = LayoutInflater.From(parent.Context).Inflate(id, parent, false);

            var vh = new QuizRecyleViewAdapterViewHolder(itemView, OnClick, OnLongClick);
            return vh;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var item = items[position];

            // Replace the contents of the view with that element
            var holder = viewHolder as QuizRecyleViewAdapterViewHolder;
            holder.ItemNo.Text = items[position].ItemNo.ToString();
            holder.ItemQuestion.Text = items[position].Question;
        }

        public override int ItemCount => items.Count;

        void OnClick(QuizRecyleViewAdapterClickEventArgs args) => ItemClick?.Invoke(this, args);
        void OnLongClick(QuizRecyleViewAdapterClickEventArgs args) => ItemLongClick?.Invoke(this, args);

    }

    public class QuizRecyleViewAdapterViewHolder : RecyclerView.ViewHolder
    {
        //public TextView TextView { get; set; }
        public TextView ItemNo { get; set; }
        public TextView ItemQuestion { get; set; }

        public QuizRecyleViewAdapterViewHolder(View itemView, Action<QuizRecyleViewAdapterClickEventArgs> clickListener,
                            Action<QuizRecyleViewAdapterClickEventArgs> longClickListener) : base(itemView)
        {
            ItemNo = itemView.FindViewById<TextView>(Resource.Id.fragment_quizitem_itemNo);
            ItemQuestion = itemView.FindViewById<TextView>(Resource.Id.fragment_quizitem_question);

            itemView.Click += (sender, e) => clickListener(new QuizRecyleViewAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
            itemView.LongClick += (sender, e) => longClickListener(new QuizRecyleViewAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
        }
    }

    public class QuizRecyleViewAdapterClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
    }
}