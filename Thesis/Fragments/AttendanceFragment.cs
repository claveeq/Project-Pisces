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
using static Android.App.DatePickerDialog;
using Thesis.Activities;
using Thesis.Adapter;

namespace Thesis.Fragments
{
    public class AttendanceFragment : Fragment
    {
        DashboardActivity dashActivity;
        Button btnDateDialog;
        ListView lvAttendance;
        AttendanceAdapter attendanceAdapter;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your fragment here
        }
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            dashActivity = Activity as DashboardActivity;
            btnDateDialog = View.FindViewById<Button>(Resource.Id.fragment_attendance_btnDateDialog);
            lvAttendance = View.FindViewById<ListView>(Resource.Id.fragment_attendance_lvAttendance);
            attendanceAdapter = new AttendanceAdapter(dashActivity);
            lvAttendance.Adapter = attendanceAdapter;

            dashActivity = Activity as DashboardActivity;
            btnDateDialog.Click += delegate
            {
#pragma warning disable CS0618 // Type or member is obsolete
                dashActivity.ShowDialog(1);
#pragma warning restore CS0618 // Type or member is obsolete
            };
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.fragment_attendance, container, false);
            return view;
        }
       
    }
}