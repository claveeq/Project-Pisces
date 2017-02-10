using Newtonsoft.Json;
using System;
using System.Collections.Generic;


[Serializable]
public class QuizData
{
    //string title;
    //List<QuizItem> items;
    //int timer; 

    public string Title { get; set; }

    //   public List<QuizItem> quizitems { get; set; }
    public string quizitems { get; set; }
    public QuizData(string title, List<QuizItem> items)
    {
        //    this.title = title;
        //    this.items = RemoveAnswersInTheList(items);
        Title = title;
        quizitems = JsonConvert.SerializeObject(items);
        // quizitems = RemoveAnswersInTheList(items); 
    }
    private List<QuizItem> RemoveAnswersInTheList(List<QuizItem> items)
    {
        var newQuizItems = new List<QuizItem>();
        foreach(var item in items)
        {
            item.Answer = string.Empty;
            //      QuizDataItem quizitem = new QuizDataItem(item.ItemNo, item.Question, item.AnsA, item.AnsB, item.AnsC, item.AnsD, item.Answer);
            newQuizItems.Add(item);
        }
        return newQuizItems;
    }
}
