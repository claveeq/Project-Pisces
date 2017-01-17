using System.Collections.Generic;
using System.Data;
using System.Linq;
namespace Thesis
{
    class Teacher
    {
        string _name;
        string _password;
        List<Subject> _subjects;

        public string GetName { get{ return _name; } }
        public string GetPassword { get { return _password; } }

        public Teacher(string name, string password)
        {
            _name = name;
            _password = password;
        }

        public List<Subject> GetMySubjects { get; set; }
    }
}