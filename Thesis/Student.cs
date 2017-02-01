using SQLite;
using System;
using Thesis;
using Thesis.Table;

[Serializable]
public class Student
{
    private int _ID; //Auto-Generated
    private string _firstName;
    private string _lastName;
    private string _macAddress; //Auto-Generated 
    private string _passcode; //Auto-Generated but can be manually edit later
    private int _teachers_ID;
    private int _currentsubject_ID;
    private short _status = 1; //Default to 1 = Absent (2 = present, 3 = late)
    private bool _inThisSubjects;

    public int CurrentSubjectID
    {
        get { return _currentsubject_ID; }
        set {  _currentsubject_ID = value;  }
    }
    public int GetID { get { return _ID; } }
    public string GetFirstName { get { return _firstName; } }
    public string GetLastName { get { return _firstName; } }
    public string GetMacAddress { get { return _macAddress; } }
    public string GetPasscode { get { return _passcode; } }

    public bool inThisSubjects
    {
        get { return _inThisSubjects; }
        set { _inThisSubjects = value; }
    }
    public void toggleInThisSubject()
    {
        inThisSubjects = DBManager.ToggleStudentInASubject(this);
    }

    public int Teachers_ID {
        get { return _teachers_ID; }
        set { _teachers_ID = value; }
    }

    public short Status {
        get { return _status; }
        set
        {
            _status = value;
        }
    }
    //instantiation of students in db
    public Student(int id)
    {
        _ID = id;
        //_macAddress = macAddress;
        _teachers_ID = DBManager.GetStudentTeachersID(_ID);
        _firstName = DBManager.GetStudentFirstName(_ID);
        _lastName = DBManager.GetStudentLastName(_ID);
        _passcode = DBManager.GetStudentPasscode(_ID);
    }
    //adding student to db
    public Student(string passcode, string firstName, string lastName, int teachersID)
    {
        //_macAddress = macAddress;
        _passcode = passcode;
        _firstName = firstName;
        _lastName = lastName;
        _teachers_ID = teachersID;

    }
    //private void retrieveStudentDataFromDB()
    //{
    //    string dpPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "local.db3"); //Call Database  
    //    var db = new SQLiteConnection(dpPath);
    //    //Student's Data from the DB
    //    var studentdata = db.Table<StudentTable>().Where(i => i.student_id == _ID).FirstOrDefault();
    //    _teachers_ID = studentdata.student_teachers_id;
    //    _firstName = studentdata.student_firstname;
    //    _lastName = studentdata.student_lastname;
    //    _passcode = studentdata.student_passcode;      
    //}

    public Student()//for creating servers
    {
        _firstName = string.Empty;
        _lastName = string.Empty;
        _macAddress = string.Empty;
        _passcode = string.Empty;
    }

    public override string ToString()
    {
        return _ID.ToString(); 
    }
}

    