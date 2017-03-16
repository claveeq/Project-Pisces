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

namespace ThesisClient.Adapter
{
    class LecturesAdapter : BaseAdapter
    {

        Context context;
        List<string> lectures;
        public LecturesAdapter(Context context, List<string> lectures)
        {
            this.context = context;
            this.lectures = lectures;
        }


        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }
        public string FileName(int position)
        {
            return lectures[position];
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;
            LecturesAdapterViewHolder holder = null;

            if(view != null)
                holder = view.Tag as LecturesAdapterViewHolder;

            if(holder == null)
            {
                holder = new LecturesAdapterViewHolder();
                var inflater = context.GetSystemService(Context.LayoutInflaterService).JavaCast<LayoutInflater>();
                //replace with your item and your holder items
                //comment back in
                view = inflater.Inflate(Resource.Layout.item_lectures, parent, false);
                holder.Title = view.FindViewById<TextView>(Resource.Id.item_lectures_tvFileName);
                view.Tag = holder;
            }


            //fill in your items
            holder.Title.Text = lectures[position];

            return view;
        }

        //Fill in cound here, currently 0
        public override int Count
        {
            get
            {
                return lectures.Count;
            }
        }

    }

    class LecturesAdapterViewHolder : Java.Lang.Object
    {
        //Your adapter views to re-use
        public TextView Title { get; set; }
    }
}