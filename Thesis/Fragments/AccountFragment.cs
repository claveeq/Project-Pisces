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

namespace Thesis.Fragments
{
    public class AccountFragment : Fragment
    {
        EditText txtUsername;
        EditText txtPassword;
        EditText txtfullName;
        Button btnSave;

        Teacher teacher;

        string username;
        string password;
        string fullName;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
         
            //txtPassword = View.FindViewById<EditText>(Resource.Id.txtPassword);
            //txtfullName = View.FindViewById<EditText>(Resource.Id.txtFullName);
            //btnSave = View.FindViewById<Button>(Resource.Id.buttonSave);

            //btnSave.Click += BtnSave_Click;
        }
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            txtUsername = View.FindViewById<EditText>(Resource.Id.txtUsername);
        }
        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                txtUsername.Text = username;
            }
        }
        //public string Password
        //{
        //    get { return password; }
        //    set
        //    {
        //        password = value;
        //        txtPassword.Text = password;
        //    }
        //}
        //public string FullName
        //{
        //    get { return fullName; }
        //    set
        //    {
        //        fullName = value;
        //        txtfullName.Text = fullName;
        //    }
        //}

        private void BtnSave_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
             return inflater.Inflate(Resource.Layout.fragment_account, container, false);
            //View view = inflater.Inflate(Resource.Layout.fragment_account, container, false);
            //return view;
        }
    }
}