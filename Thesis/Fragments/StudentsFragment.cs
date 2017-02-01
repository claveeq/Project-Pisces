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
        Subject selectedSubject;
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
            gridViewStudents = View.FindViewById<GridView>(Resource.Id.gridView_Students);
            //spinner.Prompt = "Choose Subject...";
            adapter = new SubjectSpinnerAdapter(Activity, classManager.GetSubjects);
            spinner.Adapter = adapter;
            spinner.ItemSelected += Spinner_ItemSelected;

         
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
                case (Resource.Id.nav_toggle):
                    //toggle student if he/she is in the class or not
                    if(selectedSubject.ID == 0)
                    {
                        Snackbar.Make(View, "Try changing subject first :)", Snackbar.LengthShort).Show();
                        return;
                    }
                    selectedStudent.toggleInThisSubject();
                    studentAdapter.NotifyDataSetChanged();
                    break;
                case (Resource.Id.nav_add):
                    //navigate to add fragment
                    dashActivity.ReplaceFragment(dashActivity.AddStudentFragment);
                    break;
                case (Resource.Id.nav_delete):
                    var builder = new AlertDialog.Builder(Activity);
                    builder.SetTitle("Confirm delete");
                    builder.SetMessage("Are you sure you want to delete " + selectedStudent.GetFirstName +"?");
                    builder.SetPositiveButton("Delete", (senderAlert, args) => {
                        classManager.DeleteStudent(selectedStudent);
                        studentAdapter.NotifyDataSetChanged();
                        Snackbar.Make(View, "Student Deleted", Snackbar.LengthShort).Show();
                    });

                    builder.SetNegativeButton("Cancel", (senderAlert, args) => {
                        Snackbar.Make(View, "Canceled", Snackbar.LengthShort).Show();
                    });

                    Dialog dialog = builder.Create();
                    dialog.Show();
                    break;
                case (Resource.Id.nav_edit):
                    dashActivity.ShowFragment(dashActivity.AddSubjectFragment);
                    break;
            }
        }
        private void Spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            //setting up current subject in classmanager
            selectedSubject = classManager.GetSubjects[e.Position];
            if(selectedSubject == classManager.CurrentSubject)
            {
                return;
            }
            classManager.CurrentSubject = selectedSubject;
            //refreashing the spinners
            studentAdapter.RefreshList(classManager.GetSubjectStudents);
            studentAdapter.NotifyDataSetChanged();
        }
        private void GridViewStudents_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            //var gridview = (GridView)sender;
            //   Toast.MakeText(Activity, gridview.GetItemAtPosition(e.Position).ToString(),ToastLength.Short).Show();
            selectedStudent =  classManager.GetSubjectStudents[e.Position];
            studentAdapter.selectedPosition(e.Position);
            studentAdapter.NotifyDataSetChanged();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            return inflater.Inflate(Resource.Layout.fragment_students, container, false);

            //return base.OnCreateView(inflater, container, savedInstanceState);
        }
    }
}