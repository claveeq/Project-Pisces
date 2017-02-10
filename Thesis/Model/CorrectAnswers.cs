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

namespace Thesis.Model
{
    public class CorrectAnswers
    {

        public int no;
        public string answer;  
        public CorrectAnswers(int no, string answer)
        {
            this.no = no;
            this.answer = answer;
        }
    }
}