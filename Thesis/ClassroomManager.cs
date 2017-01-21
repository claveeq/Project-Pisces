using System.Collections.Generic;

namespace Thesis
{
    public class ClassroomManager
    {
        private Teacher _teacher;
        // List of the students registered in the app

        private List<Student> _allStudents;//all registered students in the app

        private Subject _currentSubject = null;

        private List<Subject> _teachersSubjects;

        private List<Student> _activeStudents; //students who join the class
       // private List<Student> _subjectStudents;//students who are enrolled in a subject
        
        private bool classroomIsActive = false;

        //instantiate the classroom class after authentication
        public ClassroomManager(Teacher teacher)
        {
            //_subjects = subject;
            _teacher = teacher;
            //getting the list of the teacher's subjects
            _teachersSubjects = _teacher.AllSubjects;
            //getting the list of the teacher's students
            _allStudents = _teacher.AllStudents;
           
        }
        //--
        public Teacher GetTeacher { get { return _teacher; } }
        public List<Subject> GetSubjects { get { return _teachersSubjects; } }
        //------------------------active--------------------------//

        public void StartClass(Subject subject)
        {
            ServerController.SetupServer();
        }

        // retrieving current subject
        public Subject CurrentSubject
        {
            get { return _currentSubject; }
            set { _currentSubject = value;  }
        }
  
       
        //public void CheckAttendance()
        //{
        //    ServerController.GetActiveStudents;
        //    if(_currentSubject != null)
        //    {

        //        foreach(Student student in ActiveStudents)
        //        {
        //            if(_subjectStudents.Contains(student))
        //            {
        //                student.isPresent = true;
        //            }
        //        }
        //    }
        //}
        //------------------------Inactive--------------------------//
        public void AddStudent()
        {

        }
        public void AddSubject(Subject subject)
        {
            _teacher.AddSubject(subject);
        }

        public void RegisterUnregisteredStudents()
        {

        }
    }
}