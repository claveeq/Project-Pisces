using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Thesis.Activities;
using Thesis.Adapter;
using System.IO;
using Toolbar = Android.Support.V7.Widget.Toolbar;
namespace Thesis.Fragments
{
    public class LecturesFragment : Fragment
    {
        DashboardActivity dashActivity;
        ClassroomManager classManager;
        ListView lvLectures;
        LecturesAdapter lectureAdapter;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your fragment here
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            dashActivity = Activity as DashboardActivity;
            classManager = dashActivity.GetClassManager;

            lvLectures = View.FindViewById<ListView>(Resource.Id.fragment_lectures_lvLectures);
            lectureAdapter = new LecturesAdapter(dashActivity, classManager.GetLecturesFileNames());
            lvLectures.Adapter = lectureAdapter;
            lectureAdapter.NotifyDataSetChanged();

            Toolbar toolbarBottom = View.FindViewById<Toolbar>(Resource.Id.toolbar_bottom);
            toolbarBottom.InflateMenu(Resource.Menu.lectures_tools_menu);
            toolbarBottom.MenuItemClick += ToolbarBottom_MenuItemClick;
        }

        private void ToolbarBottom_MenuItemClick(object sender, Toolbar.MenuItemClickEventArgs e)
        {
            //react to click here and swap fragments or navigate
            switch(e.Item.ItemId)
            {
                case (Resource.Id.nav_refresh):
                    //toggle student if he/she is in the class or not
                    lectureAdapter.refresh(classManager.GetLecturesFileNames());
                    lectureAdapter.NotifyDataSetChanged();
                    break;
                case (Resource.Id.nav_openFolder):
                    //navigate to add fragment
                    string folderlocation;
                    if(Android.OS.Environment.ExternalStorageState.Equals(Android.OS.Environment.MediaMounted))
                        folderlocation = Android.OS.Environment.ExternalStorageDirectory.Path;
                    else
                        folderlocation = Android.OS.Environment.DirectoryDocuments;
                    folderlocation += @"/Lectures/" + classManager.GetTeacher.GetFullName + @"/";

                    if(!Directory.Exists(folderlocation))
                        Directory.CreateDirectory(folderlocation);

                    Intent intent = new Intent(Intent.ActionView);
                    var uri = Android.Net.Uri.Parse(folderlocation);
                    intent.SetDataAndType(uri, "resource/folder");
                    StartActivity(Intent.CreateChooser(intent, "Lecture Folder"));
                    break;
            }
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.fragment_lectures,container,false);
            return view;
        }
    }
}