using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using Android.Support.Design.Widget;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using ThesisClient.Fragment;

namespace ThesisClient.Activities
{
    [Activity(Label = "DashboardActivity", MainLauncher = true, Icon = "@drawable/icon")]
    public class DashboardActivity : AppCompatActivity
    {
        Toolbar toolbar;
        DrawerLayout drawerLayout;
        NavigationView navigationView;
        public FragmentTransaction fragmentTx;
        public Stack<Android.App.Fragment> stackFragments;
        public Android.App.Fragment currentFragment = new Android.App.Fragment();
        HomeFragment homeFragment;
        QuizFragment quizFragment;
        ActiveHomeFragment activeHomeFragment;

        StudentManager studentManager;

        public AuthStudent student;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Dashboard);
            initViews();
            eventHandlers();
            //Create your application here
        }
        private void initViews()
        {
            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            //Enable support action bar to display hamburger
            SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_menu_white_24dp);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            //instantiate fragments
            homeFragment = new HomeFragment();
            activeHomeFragment = new ActiveHomeFragment();
            quizFragment = new QuizFragment();

            fragmentTx = FragmentManager.BeginTransaction();
            //fragmentTx.Add(Resource.Id.fragmentContainer, quizFragment, "Students");
            //fragmentTx.Hide(quizFragment);
            fragmentTx.Add(Resource.Id.fragmentContainer, homeFragment, "Home");

            fragmentTx.Commit();

            currentFragment = homeFragment;
            stackFragments = new Stack<Android.App.Fragment>();
        }

        private void ReplaceFragment(Android.App.Fragment fragment)
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

        private void ShowFragment(Android.App.Fragment fragment)
        {
            if(fragment.IsVisible)
            {
                return;
            }
            var trans = FragmentManager.BeginTransaction();

            fragment.View.BringToFront();
            currentFragment.View.BringToFront();

            trans.Hide(currentFragment);
            trans.Show(fragment);

            trans.AddToBackStack(null);
            stackFragments.Push(currentFragment);
            trans.Commit();

            currentFragment = fragment;
        }

     
        private void eventHandlers()
        {
            navigationView.NavigationItemSelected += NavigationView_NavigationItemSelected;
        }
        public override void OnBackPressed()
        {
            //if(FragmentManager.BackStackEntryCount > 0)
            //{
            //    FragmentManager.PopBackStack();
            //    currentFragment = stackFragments.Pop();
            //}
            //else
            //{
            //    base.OnBackPressed();
            //}
            base.OnBackPressed();
        }
        private void NavigationView_NavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
        {
            e.MenuItem.SetChecked(true);
            //react to click here and swap fragments or navigate
            switch(e.MenuItem.ItemId)
            {
                case (Resource.Id.nav_home):
                    //   ShowFragment(homeFragment);
                    ReplaceFragment(homeFragment);
                    SupportActionBar.Title = "Dashboard";
                    break;
                case (Resource.Id.nav_quiz):
                    ReplaceFragment(quizFragment);
                    SupportActionBar.Title = "Quiz";
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
    }
}