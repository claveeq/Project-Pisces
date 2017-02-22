using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Thesis.Fragments;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using Android.Support.Design.Widget;
using static Android.App.DatePickerDialog;
using Android.Support.V4.View;

namespace Thesis.Activities
{
    [Activity(Label = "PICA Professor")]
    public class DashboardActivity : AppCompatActivity, IOnDateSetListener
    {
        //       
        public const int DATE_DIALOG = 1;
        private int year, month, day;
        DrawerLayout drawerLayout;
        NavigationView navigationView;
        Toolbar toolbar;
        //fragments
        public FragmentTransaction fragmentTx;
        public HomeFragment homeFragment;
        public ActiveHomeFragment activeHomeFragment;
        public StudentsFragment studentFragment;
        public ActiveStudentsFragment activeStudentsFragment;
        public AccountFragment accountFragment;
        public SubjectFragment subjectFragment;
        public AddSubjectFragment AddSubjectFragment;
        public AddStudentFragment AddStudentFragment;
        public QuizFragment QuizFragment;
        public AssignmentsFragment assignmentFragment;
        public AddAssignmentsFragment addAssignmentFragment;
        public LecturesFragment lecturesFragment;
        public ActiveQuizFragment activeQuizFragment;
        public AttendanceFragment AttendanceFragment;
        public Stack<Fragment> stackFragments;
        public Fragment currentFragment = new Fragment();
        //Essential Classes
        ClassroomManager classManager;
        Teacher loggedOnUser;

        public QuizManager quizManager;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            //UI Initialization
            //Create your application here
            SetContentView(Resource.Layout.Dashboard);
            //initializing components in layout
            InitViews();
            //Setting up toolbar
            SetSupportActionBar(toolbar);
            //Enable support action bar to display hamburger

            SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_menu_white_24dp);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            //initializing fragments in dashboard
            fragmentTx = FragmentManager.BeginTransaction();
            //fragmentTx.Add(Resource.Id.fragmentContainer, accountFragment, "Account");
            //fragmentTx.Hide(accountFragment);
            //fragmentTx.Add(Resource.Id.fragmentContainer, subjectFragment, "Subjects");
            //fragmentTx.Hide(subjectFragment);
            //fragmentTx.Add(Resource.Id.fragmentContainer, AddSubjectFragment, "AddSubject");
            //fragmentTx.Hide(AddSubjectFragment);
            //fragmentTx.Add(Resource.Id.fragmentContainer, AddStudentFragment, "AddStudent");
            //fragmentTx.Hide(AddStudentFragment);
            //fragmentTx.Add(Resource.Id.fragmentContainer, studentFragment, "Students");
            //fragmentTx.Hide(studentFragment);
            fragmentTx.Add(Resource.Id.fragmentContainer, homeFragment, "Home");
            fragmentTx.Commit();
            currentFragment = homeFragment;
            //Hanling events
            EventHanlders();
           
            //Initializing Objects
            //Getting the loggedon Teacher from Authentication
            //loggedOnUser = JsonConvert.DeserializeObject<Teacher>(Intent.GetStringExtra("Teacher"));
            //loggedOnUser.retrieveUserDataFromDB();
            string username = Intent.GetStringExtra("username") ?? "Data not available";
            string password = Intent.GetStringExtra("password") ?? "Data not available";
            loggedOnUser = new Teacher(username, password);
            classManager = new ClassroomManager(loggedOnUser);
            ServerController.classManager = classManager;
            ServerController.context = this;
            Toast.MakeText(this, "Welcome " + loggedOnUser.GetFullName + "!", ToastLength.Long).Show();
            //var list = FragmentManager.FindFragmentById<SubjectFragment>(Resource.Layout.fragment_subjects); //for communicating with fragments
            quizManager = new QuizManager(classManager.GetTeacher.GetID);
        }

        public ClassroomManager GetClassManager { get { return classManager; } }
    
        private void EventHanlders()
        {
            navigationView.NavigationItemSelected += NavigationView_NavigationItemSelected;
        }

        private void InitViews()
        {
            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);

            homeFragment = new HomeFragment();
            activeHomeFragment = new ActiveHomeFragment();
            studentFragment = new StudentsFragment();
            activeStudentsFragment = new ActiveStudentsFragment();
            accountFragment = new AccountFragment();
            subjectFragment = new SubjectFragment();
            AddSubjectFragment = new AddSubjectFragment();
            AddStudentFragment = new AddStudentFragment();
            QuizFragment = new QuizFragment();
            assignmentFragment = new AssignmentsFragment();
            addAssignmentFragment = new AddAssignmentsFragment();
            lecturesFragment = new LecturesFragment();
            activeQuizFragment = new ActiveQuizFragment();
            AttendanceFragment = new AttendanceFragment();
            stackFragments = new Stack<Fragment>();
        }
        protected override Dialog OnCreateDialog(int id)
        {
            switch(id)
            {
                case DATE_DIALOG:
                    {
                        return new DatePickerDialog(this, this, year, month, day);
                    }
                    break;
                default:
                    break;
            }
            return null;
        }

        public void OnDateSet(DatePicker view, int year, int month, int dayOfMonth)
        {
            this.year = year;
            this.month = month;
            day = dayOfMonth;
            Toast.MakeText(this, "you have selected" + (month + 1) + day + "/" + year, ToastLength.Short).Show();
        }
        public void ReplaceFragment(Fragment fragment)
        {
            if(fragment.IsVisible)
            {
                return;
            }

            var trans = FragmentManager.BeginTransaction();
            trans.Replace(Resource.Id.fragmentContainer, fragment);
            trans.AddToBackStack(null);
            trans.Commit();

            currentFragment = fragment;

        }

        public void ShowFragment(Fragment fragment)
        {
            if(fragment.IsVisible)
            {
                return;
            }

            var fragmentTx = FragmentManager.BeginTransaction();

            fragment.View.BringToFront();
            currentFragment.View.BringToFront();

            fragmentTx.Hide(currentFragment);
            fragmentTx.Show(fragment);

            fragmentTx.AddToBackStack(null);
            stackFragments.Push(currentFragment);
            fragmentTx.Commit();

            currentFragment = fragment;
        }

        public override void OnBackPressed()
        {
            var builder = new Android.Support.V7.App.AlertDialog.Builder(this);
            builder.SetTitle("Wait!");
            if(classManager.ClassroomIsActive)
            {
                builder.SetMessage("Are you sure you want to quit? We will save the class attendance for you.");
                builder.SetPositiveButton("Yes", (senderAlert, args) => {
                    classManager.SaveAttendanceToCSV();
                    this.FinishAffinity();
                });
                builder.SetNegativeButton("Cancel", (senderAlert, args) => {});
            }
            else
            {
                builder.SetMessage("Are you sure you want to quit?");
                builder.SetPositiveButton("Yes", (senderAlert, args) => {
                    this.FinishAffinity();
                });
                builder.SetNegativeButton("Cancel", (senderAlert, args) => { });
            }
            Dialog dialog = builder.Create();
            dialog.Show();
        }
    
        private void NavigationView_NavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
        {
            e.MenuItem.SetChecked(true);
            //react to click here and swap fragments or navigate
            switch(e.MenuItem.ItemId)
            {
                case (Resource.Id.nav_home):
                    if(classManager.ClassroomIsActive)
                    {
                        if(classManager.QuizIsActive)
                        {
                            ReplaceFragment(activeQuizFragment);
                            SupportActionBar.Title = "Quiz";
                        }
                        else
                        {
                            ReplaceFragment(activeHomeFragment);
                            SupportActionBar.Title = "Active Dashboard";
                        }
                    }
                    else
                    {
                        ReplaceFragment(homeFragment);
                        SupportActionBar.Title = "Dashboard";
                    }
                    break;
                case (Resource.Id.nav_students):
                  
                    if(classManager.ClassroomIsActive)
                    {
                        ReplaceFragment(activeStudentsFragment);
                        SupportActionBar.Title = "Active Students";
                    }
                    else
                    {
                        ReplaceFragment(studentFragment);
                        SupportActionBar.Title = "Students";
                    }
                    break;
                case (Resource.Id.nav_class):
                    ReplaceFragment(subjectFragment);
                    SupportActionBar.Title = "Subjects";
                    break;
                case (Resource.Id.nav_quiz):
                    ReplaceFragment(QuizFragment);
                    SupportActionBar.Title = "Quiz";
                    break;
                case (Resource.Id.nav_assignments):
                    ReplaceFragment(assignmentFragment);
                    SupportActionBar.Title = "Assignments";
                    break;
                case (Resource.Id.nav_lectures):
                    ReplaceFragment(lecturesFragment);
                    SupportActionBar.Title = "Lectures";
                    break;
                case (Resource.Id.nav_Account):
                    ReplaceFragment(accountFragment);
                    SupportActionBar.Title = "Account";
                    break;
            }
            drawerLayout.CloseDrawers();
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch(item.ItemId)
            {
                case Android.Resource.Id.Home:
                    drawerLayout.OpenDrawer(GravityCompat.Start);
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }
        private void BtnStart_Click(object sender, EventArgs e)
        {
          //  ServerController.SetupServer();
        }
    }
}