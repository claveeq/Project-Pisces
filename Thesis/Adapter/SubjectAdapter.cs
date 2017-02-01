using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;
using System;
using Android.Graphics;

namespace Thesis.Adapter
{
    internal class SubjectAdapter : BaseAdapter<Subject>
    {
        private Context context;
        private List<Subject> _subjects;
        int selected;

        public SubjectAdapter(Context context, List<Subject> subject)
        {
            this.context = context;
            _subjects = subject;
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return _subjects[position].ID;
        }

        public void selectedPosition(int postion)
        {
            selected = postion;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;
            SubjectAdapterViewHolder holder = null;

            if(view != null)
                holder = view.Tag as SubjectAdapterViewHolder;

            if(holder == null)
            {
                holder = new SubjectAdapterViewHolder();
                var inflater = context.GetSystemService(Context.LayoutInflaterService).JavaCast<LayoutInflater>();
                //replace with your item and your holder items
                //comment back in
                view = inflater.Inflate(Resource.Layout.item_subject, parent, false);
                holder.SubjectTitle = view.FindViewById<TextView>(Resource.Id.textView1);
                view.Tag = holder;
            }

            //fill in your items
            holder.SubjectTitle.Text = _subjects[position].GetTitle;

            if(position == selected)
                holder.SubjectTitle.SetBackgroundColor(Color.ParseColor("#1565c0"));
            else
                holder.SubjectTitle.SetBackgroundColor(Color.Transparent);

            return view;
        }

        //Fill in cound here, currently 0
        public override int Count
        {
            get
            {
                return _subjects.Count;
            }
        }

        public override Subject this[int position]
        {
            get
            {
                return _subjects[position];
            }
        }
    }

    internal class SubjectAdapterViewHolder : Java.Lang.Object
    {
        //Your adapter views to re-use
        public TextView SubjectTitle { get; set; }
    }
}