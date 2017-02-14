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


[Serializable]
public class Quiz
{
    private int _teachersID;
    private string _title;
    //private string _subject;
    private DateTime _dateCreated;
    int item_id;
    //
    private List<QuizItem> _items = new List<QuizItem>();

    public Quiz(int teachersID,string title, DateTime datecreated, List<QuizItem> items)
    {
        _teachersID = teachersID;
        _title = title;
        _dateCreated = datecreated;
        _items = items;
       // _subject = subject;
    }

    internal void AddItem(QuizItem item)
    {
        _items.Add(item);
    }
    public List<QuizItem> GetQuizitems { get { return _items; } }
    public int GetCurrentNumber { get { return item_id; }  }
    public string Title {
        get { return _title; }
        set { _title = value; }
    }
    public int TeachersID {
        get { return _teachersID; }
        set { _teachersID = value; }
    }
}
