using Android.App;
using Android.Widget;
using Android.OS;
using FilePickerApp.Adapter;
using System.IO;

namespace FilePickerApp
{
    [Activity(Label = "FilePickerApp", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        FileListAdapter _adapter;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            var ListAdapter = FindViewById<ListView>(Resource.Id.layout_lvFiles);
            SetContentView (Resource.Layout.Main);
            _adapter = new FileListAdapter(this, new FileSystemInfo[0]);
            ListAdapter.Adapter = _adapter;
        }
    }
}

