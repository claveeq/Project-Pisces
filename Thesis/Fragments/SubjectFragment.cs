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
namespace Thesis.Fragments
{
    public class SubjectFragment : Fragment
    {
        EditText editSearch;
        ListView listSubjects;
        List<Subject> subjects;
        Button btnToAddSubject;


        DashboardActivity DashActivity;
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

            editSearch = View.FindViewById<EditText>(Resource.Id.editSeach);
            listSubjects = View.FindViewById<ListView>(Resource.Id.listSubjects);
            btnToAddSubject = View.FindViewById<Button>(Resource.Id.buttonToAddSubject);

            btnToAddSubject.Click += BtnToAddSubject_Click;

            //   subjects = JsonConvert.DeserializeObject<List<Subject>>("subjects");
            //listSubjects.Adapter = new ArrayAdapter(Activity, Resource.Layout.item_subject, Resource.Id.textView1, data);
      
            DashActivity = (DashboardActivity)Activity;//communicating with activities
            
            classManager = DashActivity.GetClassManager;
            listSubjects.Adapter = new SubjectAdapter(Activity, classManager.GetTeacher.AllSubjects);
        }

        private void BtnToAddSubject_Click(object sender, EventArgs e)
        {
            DashActivity.ShowFragment(DashActivity.AddSubjectFragment);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            // Use this to return your custom view for this Fragment
            return inflater.Inflate(Resource.Layout.fragment_subjects, container, false); 
        }
    }
}