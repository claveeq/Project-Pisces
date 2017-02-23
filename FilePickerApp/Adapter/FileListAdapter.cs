using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace FilePickerApp.Adapter
{
    class FileListAdapter : BaseAdapter
    {

        Context context;
        private FileSystemInfo[] fileSystemInfo;

        public FileListAdapter(Context context, FileSystemInfo[] fileSystemInfo)
        {
            this.context = context;
            this.fileSystemInfo = fileSystemInfo;
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
            FileListAdapterViewHolder holder = null;

            if(view != null)
                holder = view.Tag as FileListAdapterViewHolder;

            if(holder == null)
            {
                holder = new FileListAdapterViewHolder();
                var inflater = context.GetSystemService(Context.LayoutInflaterService).JavaCast<LayoutInflater>();
                //replace with your item and your holder items
                //comment back in
                view = inflater.Inflate(Resource.Layout.item_file, parent, false);
                holder.Title = view.FindViewById<TextView>(Resource.Id.item_file_tvFileName);
                view.Tag = holder;
            }


            //fill in your items
            holder.Title.Text = fileSystemInfo[position].ToString();

            return view;
        }

        //Fill in cound here, currently 0
        public override int Count
        {
            get
            {
                return fileSystemInfo.Length;
            }
        }

    }

    class FileListAdapterViewHolder : Java.Lang.Object
    {
        //Your adapter views to re-use
        public TextView Title { get; set; }
    }
}