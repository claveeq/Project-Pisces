using Android.App;
using Android.Widget;
using Android.OS;
using System;
using SQLite;
using System.IO;
using Android.Views;
using System.Net;
using System.Linq;
using System.Net.Sockets;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using Android.Support.Design.Widget;
using Android.Content;
using Newtonsoft.Json;
namespace Thesis
{
    [Activity(Label = "Thesis", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    { 
        EditText txtusername;
        EditText txtPassword;
        Button btncreate;
        Button btnsign;
        TextView labelLogin;

        //DrawerLayout drawerLayout;
        //NavigationView navigationView;
        //Toolbar toolbar;
        //MyActionBarDrawerToggle mDrawerToggle;

        Teacher teacher;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // for changing the color of the status bar
            Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Authentication);
            // Get our button from the layout resource,  
            InitViews();

            // Setting up toolbar
            //SetSupportActionBar(toolbar);
            //SupportActionBar.Title = "Server";
            //Enable support action bar to display hamburger
            //SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_menu_white_24dp);
            //SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            
         
   
            //navigationView.NavigationItemSelected += (sender, e) => {
            //    e.MenuItem.SetChecked(true);
            //    //react to click here and swap fragments or navigate
            //    drawerLayout.CloseDrawers();
            //};
            
            // and attach an event to it  
            EventHanlders();
            //init
            ServerController.context = Application.Context;
            Auth.Init();
        }
        private void InitViews()
        {
            //drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            //navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            //toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            btnsign = FindViewById<Button>(Resource.Id.btnLogin);
            btncreate = FindViewById<Button>(Resource.Id.btnRegister);
            txtusername = FindViewById<EditText>(Resource.Id.txtUsername);
            txtPassword = FindViewById<EditText>(Resource.Id.txtPassword);
            labelLogin = FindViewById<TextView>(Resource.Id.labelLogin);
        }

        private void EventHanlders()
        {
            btnsign.Click += Btnsign_Click;
            btncreate.Click += Btncreate_Click;
            //navigationView.NavigationItemSelected += NavigationView_NavigationItemSelected;
        }

        //private void NavigationView_NavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
        //{
        //    e.MenuItem.SetChecked(true);
        //    //react to click here and swap fragments or navigate
        //    drawerLayout.CloseDrawers();
        //}

    
        //public override bool OnOptionsItemSelected(IMenuItem item)
        //{
        //    switch(item.ItemId)
        //    {
        //        case Android.Resource.Id.Home:
        //            drawerLayout.OpenDrawer(Android.Support.V4.View.GravityCompat.Start);
        //            return true;
        //    }
        //    return base.OnOptionsItemSelected(item);
        //}

        private void Btncreate_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(Activities.RegisterActivity));
        }

        private void Btnsign_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(Activities.DashboardActivity));
            //teacher = new Teacher(txtusername.Text, txtPassword.Text);
            if(Auth.AuthTeacher(txtusername.Text, txtPassword.Text))
            {
                //intent.PutExtra("Teacher", JsonConvert.SerializeObject(teacher));
                intent.PutExtra("username", txtusername.Text);
                intent.PutExtra("password", txtPassword.Text);
                StartActivity(intent);          
                Finish();
            }
            else
            {
                teacher = null;
                Snackbar.Make(btnsign, "Account doesn't exist", 0).Show();
            }

        }
    }
}

