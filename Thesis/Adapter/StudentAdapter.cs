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
using Android.Graphics.Drawables;
using Android.Util;
using Android.Graphics;

namespace Thesis.Adapter
{
    class StudentAdapter : BaseAdapter<Student>
    {
        private Context context;
        private List<Student> _students;
        private ClassroomManager classManager;
        private bool _isActive = false;
        int selected = -1; // select nothing
        public StudentAdapter(Context context, List<Student> students)
        {
            this.context = context;
            _students = students;
        }
        public StudentAdapter(Context context, List<Student> students, bool isActive)
        {
            this.context = context;
            _students = students;
            _isActive = isActive;
        }
        public StudentAdapter(Context context, List<Student> students, ClassroomManager classManager)
        {
            this.context = context;
            _students = students;
            this.classManager = classManager;
        }
        public override Student this[int position]
        {
            get
            {
                return _students[position];
            }
        }
        public void selectedPosition(int postion)
        {
            selected = postion;
        }
        public override long GetItemId(int position)
        {
            return _students[position].GetID;
        }

        public void RefreshList(List<Student> List)
        {
            _students.Clear();
            _students = List;
            selected = -1;
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;
            StudentAdapterViewHolder holder = null;

            if(view != null)
                holder = view.Tag as StudentAdapterViewHolder;

            if(holder == null)
            {
                holder = new StudentAdapterViewHolder();
                var inflater = context.GetSystemService(Context.LayoutInflaterService).JavaCast<LayoutInflater>();
                //replace with your item and your holder items
                //comment back in
                view = inflater.Inflate(Resource.Layout.item_students, parent, false);
                holder.ImageStatus = view.FindViewById<ImageView>(Resource.Id.imageStudentStatus);
                holder.inTheSubject = view.FindViewById<ImageView>(Resource.Id.inThisSubject);
                holder.Name = view.FindViewById<TextView>(Resource.Id.textStudentName);
                view.Tag = holder;
            }
            //fill in your items //attendance status
            switch(_students[position].Status)
            {
                default://absent
                    holder.ImageStatus.SetImageResource(Resource.Drawable.ic_account_box_grey_800_24dp);
                    break;
                case 2://present
                    holder.ImageStatus.SetImageResource(Resource.Drawable.ic_account_box_lime_A700_24dp);
                    break;
                case 3://late
                    holder.ImageStatus.SetImageResource(Resource.Drawable.ic_account_box_amber_200_24dp);
                    break;
            }
            //if a student is selected
            if(position == selected)
            {
                holder.ImageStatus.SetBackgroundColor(Color.ParseColor("#bbdefb"));
            }
            else
            {
                holder.ImageStatus.SetBackgroundColor(Color.Transparent);
            }
    
            //If the student is in the subject
            if(_students[position].inThisSubjects == true)
            {
                holder.inTheSubject.Visibility = ViewStates.Visible;
            }
            else
            {
                holder.inTheSubject.Visibility = ViewStates.Invisible;
            }

            holder.Name.Text = _students[position].GetFirstName;

            return view;
        }
       
        //Fill in cound here, currently 0
        public override int Count
        {
            get
            {
                return _students.Count;
            }
        }   
    }

    class StudentAdapterViewHolder : Java.Lang.Object
    {
        //Your adapter views to re-use
        public ImageView ImageStatus { get; set; }
        public ImageView inTheSubject { get; set; }
        public TextView Name { get; set; }
    }
}