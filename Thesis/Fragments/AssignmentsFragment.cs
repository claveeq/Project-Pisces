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
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.Design.Widget;
using Thesis.Adapter;
using Thesis.Activities;
using Thesis.Model;

namespace Thesis.Fragments
{
    public class AssignmentsFragment : Fragment
    {
        ListView lvAssignments;
       // SubjectSpinnerAdapter adapter;
        Toolbar toolbarBottom;
        AssignmentAdapter assignmentAdapter;
        ClassroomManager classManager;
        DashboardActivity dashActivity;
        Assignment selectedAssignment;

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
            lvAssignments = View.FindViewById<ListView>(Resource.Id.fragment_assignment_lvAssignments);
            assignmentAdapter = new AssignmentAdapter(dashActivity, classManager.GetAssignments());
            lvAssignments.Adapter = assignmentAdapter;
            lvAssignments.ItemClick += LvAssignments_ItemClick;

            toolbarBottom = View.FindViewById<Toolbar>(Resource.Id.fragment_assignment_tbAssignment);
            toolbarBottom.InflateMenu(Resource.Menu.assignment_tools_menu);
            toolbarBottom.MenuItemClick += ToolbarBottom_MenuItemClick;

            
        }

        private void LvAssignments_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            assignmentAdapter.selectedPosition(e.Position);
            selectedAssignment = classManager.GetAssignments()[e.Position];
            assignmentAdapter.NotifyDataSetChanged();
        }

        private void ToolbarBottom_MenuItemClick(object sender, Toolbar.MenuItemClickEventArgs e)
        {
            switch(e.Item.ItemId)
            {
                case (Resource.Id.nav_add):
                    //navigate to add fragment

                    dashActivity.ReplaceFragment(dashActivity.addAssignmentFragment);
                    break;
                case (Resource.Id.nav_delete):
                    if(selectedAssignment ==null)
                    {
                        Snackbar.Make(View, "Choose an assignment you want to delete.", Snackbar.LengthShort).Show();
                        return;
                    }
                    var builder = new AlertDialog.Builder(Activity);
                    builder.SetTitle("Confirm delete");
                    builder.SetMessage("Are you sure you want to delete " + selectedAssignment.Title + "?");
                    builder.SetPositiveButton("Delete", (senderAlert, args) =>
                    {
                        classManager.DeleteAssignment(selectedAssignment);
                        selectedAssignment = null;
                        assignmentAdapter.RefreshList(classManager.GetAssignments());
                        assignmentAdapter.NotifyDataSetChanged();
                        Snackbar.Make(View, "Assignment Deleted", Snackbar.LengthShort).Show();
                    });

                    builder.SetNegativeButton("Cancel", (senderAlert, args) =>
                    {
                        Snackbar.Make(View, "Canceled", Snackbar.LengthShort).Show();
                    });

                    Dialog dialog = builder.Create();
                    dialog.Show();
                    break;
                case (Resource.Id.nav_edit):
                    dashActivity.ReplaceFragment(dashActivity.addAssignmentFragment);
                    break;
            }
        }
   
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            View view = inflater.Inflate(Resource.Layout.fragment_assignment, container, false);
            return view;
        }
    }
}