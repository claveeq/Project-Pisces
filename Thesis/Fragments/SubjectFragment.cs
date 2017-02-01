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

namespace Thesis.Fragments
{
    public class SubjectFragment : Fragment
    {
        EditText editSearch;
        ListView listSubjects;
        Button btnToAddSubject;
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

            editSearch = View.FindViewById<EditText>(Resource.Id.editSeach);
            listSubjects = View.FindViewById<ListView>(Resource.Id.listSubjects);
            btnToAddSubject = View.FindViewById<Button>(Resource.Id.buttonToAddSubject);

            btnToAddSubject.Click += BtnToAddSubject_Click;

            //   subjects = JsonConvert.DeserializeObject<List<Subject>>("subjects");
            //listSubjects.Adapter = new ArrayAdapter(Activity, Resource.Layout.item_subject, Resource.Id.textView1, data);

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
                    dashActivity.ReplaceFragment(dashActivity.AddStudentFragment);
                    break;
                case (Resource.Id.nav_delete):
                    //dashActivity.ShowFragment(dashActivity.AddSubjectFragment);
                    classManager.DeleteSubject(selectedSubject);
                   classManager.GetSubjects.Remove(selectedSubject);
                    subjectAdapter.NotifyDataSetChanged();
                    break;
                case (Resource.Id.nav_edit):
                    //dashActivity.ShowFragment(dashActivity.AddSubjectFragment);
                    break;
            }
        }

        private void BtnToAddSubject_Click(object sender, EventArgs e)
        {
        
            dashActivity.ReplaceFragment(dashActivity.AddSubjectFragment);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            return inflater.Inflate(Resource.Layout.fragment_subjects, container, false); 
        }
    }
}