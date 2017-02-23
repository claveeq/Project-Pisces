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

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.fragment_lectures,container,false);
            return view;
        }
    }
}