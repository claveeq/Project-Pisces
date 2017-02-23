using Android.Views;
using Java.Lang;

namespace FilePickerApp.Adapter
{
    interface IFileListAdapter
    {
        int Count { get; }

        Object GetItem(int position);
        long GetItemId(int position);
        View GetView(int position, View convertView, ViewGroup parent);
    }
}