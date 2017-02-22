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
using Newtonsoft.Json;
using Thesis.Activities;
using Thesis.Adapter;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.Design.Widget;

namespace Thesis.Fragments
{
    public class SubjectFragment : Fragment
    {
        ListView listSubjects;
        Subject selectedSubject;
        SubjectAdapter subjectAdapter;
        DashboardActivity dashActivity;
        ClassroomManager classManager;
        string[] data = { };

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            listSubjects = View.FindViewById<ListView>(Resource.Id.listSubjects);          

            dashActivity = (DashboardActivity)Activity;//communicating with activities
            
            classManager = dashActivity.GetClassManager;
            subjectAdapter = new SubjectAdapter(Activity, classManager.GetSubjects);
            listSubjects.Adapter = subjectAdapter;
            listSubjects.ItemClick += ListSubjects_ItemClick;
                  
            Toolbar toolbarBottom = View.FindViewById<Toolbar>(Resource.Id.toolbar_bottom);
            toolbarBottom.InflateMenu(Resource.Menu.subject_tools_menu);
            toolbarBottom.MenuItemClick += ToolbarBottom_MenuItemClick;
        }

        private void ListSubjects_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            selectedSubject = classManager.GetSubjects[e.Position];
            subjectAdapter.selectedPosition(e.Position);
            subjectAdapter.NotifyDataSetChanged();
        }

        private void ToolbarBottom_MenuItemClick(object sender, Toolbar.MenuItemClickEventArgs e)
        {
            switch(e.Item.ItemId)
            {
                case (Resource.Id.nav_add):
                    dashActivity.ReplaceFragment(dashActivity.AddSubjectFragment);
                    break;
                case (Resource.Id.nav_delete):
                    if(selectedSubject == null)
                    {
                        Snackbar.Make(View, "Select a subject first.", Snackbar.LengthShort).Show();
                        return;
                    }
                    classManager.DeleteSubject(selectedSubject);
                    classManager.GetSubjects.Remove(selectedSubject);
                    subjectAdapter.NotifyDataSetChanged();
                    break;
                //case (Resource.Id.nav_edit):
                //    if(selectedSubject == null)
                //    {
                //        Snackbar.Make(View, "Select a subject first.", Snackbar.LengthShort).Show();
                //        return;
                //    }
                //    classManager.UpdateSubject(selectedSubject);
                //    dashActivity.ReplaceFragment(dashActivity.AddSubjectFragment);
                //    break;
            }
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.fragment_subjects, container, false);
            return view;
        }
    }
}