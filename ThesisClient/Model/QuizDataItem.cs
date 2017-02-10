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
public class QuizDataItem
{
    public int ItemNo { get; set; }
    public string Question { get; set; }
    public string AnsA { get; set; }
    public string AnsB { get; set; }
    public string AnsC { get; set; }
    public string AnsD { get; set; }
    public string Answer { get; set; }

    public QuizDataItem(int itemNo, string question, string ansA, string ansB, string ansC, string ansD, string answer)
    {
        ItemNo = itemNo;
        Question = question;
        AnsA = ansA;
        AnsB = ansB;
        AnsC = ansC;
        AnsD = ansD;
        Answer = answer;
    }
        
}
