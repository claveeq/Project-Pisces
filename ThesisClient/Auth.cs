using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThesisClient
{
    static class Auth
    {
        public static bool AuthStudent(Student student)
        {
            string studentdb = "1234";
            if(student.GetPasscode == studentdb)
            {
                return true;
            }
            return false;
        }
    }
}
