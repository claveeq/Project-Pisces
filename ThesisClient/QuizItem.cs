using System;
[Serializable]
public class QuizItem
{
    int _itemNo;
    string _question;
    string _answerA;
    string _answerB;
    string _answerC;
    string _answerD;
    string _correctAnswer;

    public QuizItem(int no, string question, string ansA, string ansB,string ansC,string ansD,string correctAnswer)
    {
        _itemNo = no;
        _question = question;
        _answerA = ansA;
        _answerB = ansB;
        _answerC = ansC;
        _answerD = ansD;
        _correctAnswer = correctAnswer;
    }
    public int ItemNo
    {
        get { return _itemNo; }
        set { _itemNo = value; }
    }

    public string Question
    {
        get { return _question; }
        set { _question = value; }
    }
    public string AnsA
    {
        get { return _answerA; }
        set { _answerA = value; }
    }
    public string AnsB
    {
        get { return _answerB; }
        set { _answerB = value; }
    }
    public string AnsC
    {
        get { return _answerC; }
        set { _answerC = value; }
    }
    public string AnsD
    {
        get { return _answerD; }
        set { _answerD = value; }
    }
    public string Answer
    {
        get { return _correctAnswer; }
        set { _correctAnswer = value; }
    }
}
