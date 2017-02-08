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
using ThesisClient.Model;

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
        //Fragments
        public HomeFragment homeFragment;
        public QuizFragment quizFragment;
        public ActiveHomeFragment activeHomeFragment;
        public SettingsFragment settingsFragment;
        public AccountSetupFragment accountSetupFragment;
        public StudentManager studentManager;
        public AccountFragment accountFragment;

        public AuthStudent student;
        public Settings settings;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Dashboard);
          
            //Create your application here
     
            if(BinarySerializer.SettingsExist())
            {
                settings = BinarySerializer.DeserializeSettings();
            }
            if(settings != null)
            {
                studentManager = new StudentManager(settings);
            }
            initViews();
            eventHandlers();
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
            settingsFragment = new SettingsFragment();
            quizFragment = new QuizFragment();
            accountSetupFragment = new AccountSetupFragment();
            accountFragment = new AccountFragment();
            fragmentTx = FragmentManager.BeginTransaction();

            if(settings != null)
            {
                fragmentTx.Add(Resource.Id.fragmentContainer, homeFragment, "Home");
                currentFragment = homeFragment;
            }
            else
            {
                fragmentTx.Add(Resource.Id.fragmentContainer, accountSetupFragment, "Setup");
                currentFragment = accountSetupFragment;
            }
           
            fragmentTx.Commit();
            stackFragments = new Stack<Android.App.Fragment>();
        }

        public void ReplaceFragment(Android.App.Fragment fragment)
        {
            if(fragment.IsVisible)
                return;

            var trans = FragmentManager.BeginTransaction();
            trans.Replace(Resource.Id.fragmentContainer, fragment);
            trans.AddToBackStack(null);
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
                    if(studentManager.Status == appStatus.inactive)
                    {
                        if(BinarySerializer.SettingsExist())
                            ReplaceFragment(homeFragment);
                        else
                            ReplaceFragment(accountSetupFragment);
                    }
                    else
                    {
                        ReplaceFragment(activeHomeFragment);
                    }
                    SupportActionBar.Title = "Dashboard";
                    break;
                case (Resource.Id.nav_quiz):
                    ReplaceFragment(quizFragment);
                    SupportActionBar.Title = "Quiz";
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
                    drawerLayout.OpenDrawer(Android.Support.V4.View.GravityCompat.Start);
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}