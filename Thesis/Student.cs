using System;

[Serializable]
class Student
{
    private string _name;
    private string _macAddress;
    private string _passcode;
    //private int  _age;
    //private string _gender;
    //private string _birthday;
    private bool present;
        
    public string GetName { get { return _name; } }
    public string GetMacAddress { get { return _macAddress; } }
    public string GetPasscode { get { return _passcode; } }
    //public int GetAge { get { return _age; } }
    //public string GetGender { get { return _gender; } }
    //public string GetBirthday { get { return _birthday; } }
    public bool isPresent
    {
        get { return present; }
        set { present = value; }
    }


    public Student(string name, string macAddress, string passcode)
    {
        _name = name;
        _macAddress = macAddress;
        _passcode = passcode;
        //_age = age;
        //_gender = gender;
        //_birthday = birthday;
    }

    public Student()
    {
        _name = string.Empty;
        _macAddress = string.Empty;
        _passcode = string.Empty;
    }

}

    