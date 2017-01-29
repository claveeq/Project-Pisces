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
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.Design.Widget;

namespace Thesis.Fragments
{
    public class StudentsFragment : Fragment
    {
        Spinner spinner;
        GridView gridViewStudents;
        StudentAdapter studentAdapter;

        ClassroomManager classManager;
        DashboardActivity dashActivity;
        SubjectSpinnerAdapter adapter;
        Student selectedStudent;

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

            spinner = View.FindViewById<Spinner>(Resource.Id.spinner_Subjects);
            //spinner.Prompt = "Choose Subject...";
            var adapter = new SubjectSpinnerAdapter(Activity, classManager.GetSubjects);
            spinner.Adapter = adapter;
            
            spinner.ItemSelected += Spinner_ItemSelected;

            gridViewStudents = View.FindViewById<GridView>(Resource.Id.gridView_Students);
            studentAdapter = new StudentAdapter(Activity, classManager.GetSubjectStudents, classManager);

            gridViewStudents.Adapter = studentAdapter;
            
            gridViewStudents.ItemClick += GridViewStudents_ItemClick;
            Toolbar toolbarBottom = View.FindViewById<Toolbar>(Resource.Id.toolbar_bottom);
            toolbarBottom.InflateMenu(Resource.Menu.students_tools_menu);
            toolbarBottom.MenuItemClick += ToolbarBottom_MenuItemClick;
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
                    classManager.DeleteStudent(selectedStudent);
                    break;
                case (Resource.Id.nav_edit):
                    //dashActivity.ShowFragment(dashActivity.AddSubjectFragment);
                    break;
            }
        }
        private void Spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            //var spinner = (Spinner)sender;
            //string toast = string.Format(classManager.GetSubjects[e.Position].ID.ToString());
            //Toast.MakeText(Activity, toast, ToastLength.Short).Show();
            //classManager.StudentsInASubject(classManager.GetSubjects[e.Position].GetID);
            var subject = classManager.GetSubjects[e.Position];
            classManager.CurrentSubject = subject;
            //refreashing the spinners
            studentAdapter.NotifyDataSetChanged();
        }
        private void GridViewStudents_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            //var gridview = (GridView)sender;
            //   Toast.MakeText(Activity, gridview.GetItemAtPosition(e.Position).ToString(),ToastLength.Short).Show();
            classManager.GetSubjectStudents[e.Position].toggleInThisSubject();
            Toast.MakeText(Activity, classManager.GetSubjectStudents[e.Position].inThisSubjects.ToString(), ToastLength.Short).Show();
            selectedStudent =  classManager.GetSubjectStudents[e.Position];
            studentAdapter.selectedPosition(e.Position);
            studentAdapter.NotifyDataSetChanged();
        }



        //private void Spinner_Click(object sender, EventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        //private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        //{
        //    var spinner = sender as Spinner;
        //    //Toast.MakeText(Activity, "You chosed:" + spinner.GetItemAtPosition(e.Position), ToastLength.Short).Show();
        //    Toast.MakeText(Activity, "You chosed:" + spinner.Adapter.GetItem(e.Position), ToastLength.Short).Show();
        //}

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            return inflater.Inflate(Resource.Layout.fragment_students, container, false);

            //return base.OnCreateView(inflater, container, savedInstanceState);
        }
    }
}