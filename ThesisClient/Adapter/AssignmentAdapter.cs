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
using Android.Graphics;
using ThesisClient.Model;

namespace ThesisClient.Adapter
{
    class AssignmentAdapter : BaseAdapter
    {

        Context context;
        List<Assignment> assignments;
        int selected = -1;
        public AssignmentAdapter(Context context, List<Assignment> assignments)
        {
            this.context = context;
            this.assignments = assignments;
        }


        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }
        public void selectedPosition(int postion)
        {
            selected = postion;
        }
        public void RefreshList(List<Assignment> assignments)
        {
            selected = -1;
            this.assignments.Clear();
            this.assignments = assignments;
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;
            AssignmentAdapterViewHolder holder = null;

            if(view != null)
                holder = view.Tag as AssignmentAdapterViewHolder;

            if(holder == null)
            {
                holder = new AssignmentAdapterViewHolder();
                var inflater = context.GetSystemService(Context.LayoutInflaterService).JavaCast<LayoutInflater>();
                //replace with your item and your holder items
                //comment back in
                view = inflater.Inflate(Resource.Layout.item_assignments, parent, false);
                holder.Title = view.FindViewById<TextView>(Resource.Id.item_assignment_etTitle);
                holder.DateCreated = view.FindViewById<TextView>(Resource.Id.item_assignment_etDateCreated);
                holder.Subject = view.FindViewById<TextView>(Resource.Id.item_assignment_etSubject);
                holder.Description = view.FindViewById<TextView>(Resource.Id.item_assignment_etDescription);
                view.Tag = holder;
            }

            if(position == selected)
            {
                holder.Title.SetBackgroundColor(Color.ParseColor("#bbdefb"));
                holder.DateCreated.SetBackgroundColor(Color.ParseColor("#bbdefb"));
                holder.Subject.SetBackgroundColor(Color.ParseColor("#bbdefb"));
                holder.Description.SetBackgroundColor(Color.ParseColor("#bbdefb"));
            }
            else
            {
                holder.Title.SetBackgroundColor(Color.Transparent);
                holder.DateCreated.SetBackgroundColor(Color.Transparent);
                holder.Subject.SetBackgroundColor(Color.Transparent);
                holder.Description.SetBackgroundColor(Color.Transparent);
            }
               

            //fill in your items
            holder.Title.Text = assignments[position].Title;
            holder.DateCreated.Text = assignments[position].DateCreated;
            holder.Subject.Text = assignments[position].Subjct;
            holder.Description.Text = assignments[position].Description;
            return view;
        }

        //Fill in cound here, currently 0
        public override int Count
        {
            get
            {
                return assignments.Count;
            }
        }

    }

    class AssignmentAdapterViewHolder : Java.Lang.Object
    {
        //Your adapter views to re-use
        public TextView Title { get; set; }
        public TextView DateCreated { get; set; }
        public TextView Subject { get; set; }
        public TextView Description { get; set; }
    }
}