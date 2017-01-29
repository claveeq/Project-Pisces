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
using Thesis.Adapter;
using Thesis.Activities;
using Toolbar = Android.Support.V7.Widget.Toolbar;
namespace Thesis.Fragments
{
    public class ActiveStudentsFragment : Fragment
    {
        GridView gridViewStudents;
        StudentAdapter studentAdapter;
        Toolbar toolbarBottom;

        Student selectedStudent;
        DashboardActivity dashActivity;
        ClassroomManager classManager;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            dashActivity = (DashboardActivity)Activity;
            classManager = dashActivity.GetClassManager;

            gridViewStudents = View.FindViewById<GridView>(Resource.Id.gridView_Students);
            studentAdapter = new StudentAdapter(Activity, classManager.GetSubjectStudents, classManager.ClassroomIsActive);
            gridViewStudents.Adapter = studentAdapter;

            toolbarBottom = View.FindViewById<Toolbar>(Resource.Id.fragment_students_active_toolbar_bottom);
            toolbarBottom.InflateMenu(Resource.Menu.students_tools_menu);
            toolbarBottom.MenuItemClick += ToolbarBottom_MenuItemClick;
           
            gridViewStudents.ItemClick += GridViewStudents_ItemClick;
        }

        private void ToolbarBottom_MenuItemClick(object sender, Toolbar.MenuItemClickEventArgs e)
        {
            //react to click here and swap fragments or navigate
            switch(e.Item.ItemId)
            {
                case (Resource.Id.nav_add):
                    dashActivity.ReplaceFragment(dashActivity.AddStudentFragment);
                    break;
                case (Resource.Id.nav_delete):
                    //dashActivity.ShowFragment(dashActivity.AddSubjectFragment);
                    break;
                case (Resource.Id.nav_edit):
                    //dashActivity.ShowFragment(dashActivity.AddSubjectFragment);
                    break;
            }
        }

        private void GridViewStudents_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            classManager.GetSubjectStudents[e.Position].toggleInThisSubject();
            Toast.MakeText(Activity, classManager.GetSubjectStudents[e.Position].inThisSubjects.ToString(), ToastLength.Short).Show();
            selectedStudent = classManager.GetSubjectStudents[e.Position];
            studentAdapter.selectedPosition(e.Position);
            studentAdapter.NotifyDataSetChanged();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            return inflater.Inflate(Resource.Layout.fragment_students_active, container, false);
        }
    }
}