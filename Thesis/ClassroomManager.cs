using System.Collections.Generic;

namespace Thesis
{
    internal class ClassroomManager
    {
        private Teacher _teacher;
        private Subject _currentSubject;
        private List<Student> _activeStudents;
        private List<Student> _subjectStudents;

        public Subject SetCurrentSubject { set { _currentSubject = value;  } }
        //need to continiously update na variable
        public List<Student> SetStudent
        {
            private get
            {
                return _activeStudents;
            }
            set
            {
                _activeStudents = ServerController.GetActiveStudents;
            }
        }

        public void StartClass()
        {
            ServerController.SetupServer();
           
        }

        public ClassroomManager(Teacher teacher)
        {
        //    _subjects = subject;
            _teacher = teacher;
        }

        public void CreateSubject()
        {
            
        }
        
        public void RegisterUnregisteredStudents()
        {

        }

        public void CheckAttendance()
        {
            if(_currentSubject != null)
            {
                _activeStudents = ServerController.GetActiveStudents;
                foreach(Student student in _activeStudents)
                {
                    if(_activeStudents.Contains(student))
                    {
                        student.isPresent = true;
                    }
                }
            }
        }
    }
}