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

namespace Thesis.Activites
{
    [Activity(Label = "Thesis", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/MyCustomTheme")]
    public class MainActivity : Activity
    {
       

        EditText txtusername;
        EditText txtPassword;
        Button btncreate;
        Button btnsign;
        TextView labelLogin;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            

            // Get our button from the layout resource,  
            // and attach an event to it  
            btnsign = FindViewById<Button>(Resource.Id.btnLogin);
            btncreate = FindViewById<Button>(Resource.Id.btnRegister);
            txtusername = FindViewById<EditText>(Resource.Id.txtUsername);
            txtPassword = FindViewById<EditText>(Resource.Id.txtPassword);
            labelLogin = FindViewById<TextView>(Resource.Id.labelLogin);

            btnsign.Click += Btnsign_Click;
            btncreate.Click += Btncreate_Click;

            CreateDB();
        }

        public string CreateDB()
        {
            var output = "";
            output += "Creating Databse if it doesnt exists";
            string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "user.db3"); //Create New Database  
          
            var db = new SQLiteConnection(dpPath);
            output += "\n Database Created....";
            return output;
        }

        private void Btncreate_Click(object sender, System.EventArgs e)
        {
            StartActivity(typeof(Activities.RegisterActivity));

        }

        private void Btnsign_Click(object sender, System.EventArgs e)
        {
            try
            {
                string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "user.db3"); //Call Database  
                var db = new SQLiteConnection(dpPath);
                var data = db.Table<Table.LoginTable>(); //Call Table  
                var data1 = data.Where(x => x.username == txtusername.Text && x.password == txtPassword.Text).FirstOrDefault(); //Linq Query  
                if(data1 != null)
                {
                    StartActivity(typeof(Activities.DashboardActivity));
                    Toast.MakeText(this, "Login Success", ToastLength.Short).Show();
                }
                else
                {
                    Toast.MakeText(this, "Username or Password invalid", ToastLength.Short).Show();
                }
            }
            catch(Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }
        }
    }
}

