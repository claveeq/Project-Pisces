using System;
[Serializable]
public class AuthStudent
{
    private string _macAddress;
    private string _passcode;
    //private int  _age;
    //private string _gender;
    //private string _birthday;

    public string GetMacAddress { get { return _macAddress; } }
    public string GetPasscode { get { return _passcode; } }
    //public int GetAge { get { return _age; } }
    //public string GetGender { get { return _gender; } }
    //public string GetBirthday { get { return _birthday; } }

    public AuthStudent(string macAddress, string passcode)
    {
        _macAddress = macAddress;
        _passcode = passcode;
        //_age = age;
        //_gender = gender;
        //_birthday = birthday;
    }
    public AuthStudent()
    {
        _macAddress = string.Empty;
        _passcode = string.Empty;
    }
}

