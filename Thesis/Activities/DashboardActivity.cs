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

namespace Thesis.Activities
{
    [Activity(Label = "DashboardActivity")]
    public class DashboardActivity : ActionBarActivity
    {
        DrawerLayout drawerLayout;
        NavigationView navigationView;
        Toolbar toolbar;
        MyActionBarDrawerToggle mDrawerToggle;

        FragmentTransaction fragmentTx;
        HomeFragment homeFragment;
        StudentsFragment studentFragment;
        Stack<Fragment> stackFragments;
        Fragment currentFragment = new Fragment();
        //Essential Classes
        ClassroomManager CM;
        Teacher teacher;
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
            SupportActionBar.Title = "Server";
            //Enable support action bar to display hamburger
            SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_menu_white_24dp);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            //initializing fragments in dashboard
            fragmentTx = FragmentManager.BeginTransaction();
            fragmentTx.Add(Resource.Id.fragmentContainer, studentFragment, "Students");
            fragmentTx.Hide(studentFragment);
            fragmentTx.Add(Resource.Id.fragmentContainer, homeFragment, "Home");
            fragmentTx.Commit();
            currentFragment = homeFragment;
            //Hanling events
            EventHanlders();
            //Initializing Objects
            CM = new ClassroomManager(teacher);
        }

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
            studentFragment = new StudentsFragment();
            stackFragments = new Stack<Fragment>();
        }

        private void ShowFragment(Fragment fragment)
        {

            if(fragment.IsVisible)
            {
                return;
            }

            var fragmentTx = FragmentManager.BeginTransaction();

            //fragment.View.BringToFront();
            //currentFragment.View.BringToFront();

            fragmentTx.Hide(currentFragment);
            fragmentTx.Show(fragment);

            fragmentTx.AddToBackStack(null);
            stackFragments.Push(currentFragment);
            fragmentTx.Commit();

            currentFragment = fragment;

            stackFragments.Push(currentFragment);
        }

        public override void OnBackPressed()
        {
            if(FragmentManager.BackStackEntryCount>0)
            {
                FragmentManager.PopBackStack();
                currentFragment = stackFragments.Pop();
            }
            else
            {
                base.OnBackPressed();
            }
        }

        private void NavigationView_NavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
        {
            e.MenuItem.SetChecked(true);
            //react to click here and swap fragments or navigate
            switch(e.MenuItem.ItemId)
            {
                case (Resource.Id.nav_home):
                    ShowFragment(homeFragment);
                    SupportActionBar.Title = "Dashboard";
                    break;
                case (Resource.Id.nav_students):
                    ShowFragment(studentFragment);
                    SupportActionBar.Title = "Students";
                    break;
            }
            drawerLayout.CloseDrawers();
        }


        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch(item.ItemId)
            {
                case Android.Resource.Id.Home:
                    drawerLayout.OpenDrawer(Android.Support.V4.View.GravityCompat.Start);
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