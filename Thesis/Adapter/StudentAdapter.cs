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

namespace Thesis.Adapter
{
    class StudentAdapter : BaseAdapter<Student>
    {
        private Context context;
        private List<Student> _students;
        private ClassroomManager classManager;
    
        public StudentAdapter(Context context, List<Student> students)
        {
            this.context = context;
            _students = students;


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
        public override long GetItemId(int position)
        {
            return _students[position].GetID;
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
                holder.Name = view.FindViewById<TextView>(Resource.Id.textStudentName);
                view.Tag = holder;
            }
            //fill in your items
            switch(_students[position].GetStatus)
            {
                default:
                    holder.ImageStatus.SetImageResource(Resource.Drawable.ic_account_box_grey_800_24dp);
                    break;
                case 2:
                    holder.ImageStatus.SetImageResource(Resource.Drawable.ic_account_box_amber_900_24dp);
                    break;
                case 3:
                    holder.ImageStatus.SetImageResource(Resource.Drawable.ic_account_box_amber_200_24dp);
                    break;

            }

            //if(_students[position].CurrentSubjectID == classManager.CurrentSubject.GetID)
            //    holder.ImageStatus.SetImageResource(Resource.Drawable.ic_account_box_lime_A700_24dp);
            //else
            //    holder.ImageStatus.SetImageResource(Resource.Drawable.ic_account_box_grey_800_24dp);

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
        public TextView Name { get; set; }
    }
}