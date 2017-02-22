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
using ThesisClient.Adapter;

namespace ThesisClient.Fragment
{
    public class AssignmentFragment : Android.App.Fragment
    {
        AssignmentAdapter assignmentAdapter;
        ListView lvAssignments;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your fragment here
        }
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            lvAssignments = View.FindViewById<ListView>(Resource.Id.fragment_assignment_lvAssignments);
            assignmentAdapter = new AssignmentAdapter(Activity, ClientController.assignments);
            lvAssignments.Adapter = assignmentAdapter;
            assignmentAdapter.NotifyDataSetChanged();

        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            return inflater.Inflate(Resource.Layout.fragment_assignment, container, false); 
        }
    }
}