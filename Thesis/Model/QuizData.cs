using Newtonsoft.Json;
using System;
using System.Collections.Generic;


[Serializable]
public class QuizData
{
    //string title;
    public List<QuizItem> items;
    //int timer; 

    public string Title { get; set; }
    public string quizitems { get; set; }

    public QuizData(string title, List<QuizItem> items, bool removeAnswers)
    {
        //    this.title = title;
        //    this.items = RemoveAnswersInTheList(items);
        Title = title;
        if(removeAnswers)
            RemoveAnswersInTheList(items);

        quizitems = JsonConvert.SerializeObject(items);
        // quizitems = RemoveAnswersInTheList(items); 
    }
    private List<QuizItem> RemoveAnswersInTheList(List<QuizItem> items)
    {
        var newQuizItems = new List<QuizItem>();
        foreach(var item in items)
        {
            item.Answer = string.Empty;        
            newQuizItems.Add(item);
        }
        return newQuizItems;
    }

    public void DezerializeListItems()
    {
        items = JsonConvert.DeserializeObject<List<QuizItem>>(quizitems);
    }
}
