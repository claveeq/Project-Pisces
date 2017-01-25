using SQLite;

namespace Thesis.Table
{
    class StudentTable
    {
        [PrimaryKey, AutoIncrement, Column("_Id")]

        public int student_id { get; set; } // AutoIncrement and set primarykey  

        [MaxLength(25), NotNull]

        public string student_passcode { get; set; }

        [MaxLength(25), NotNull]

        public string student_firstname { get; set; }

        [MaxLength(25) ,NotNull]

        public string student_lastname { get; set; }

        [MaxLength(25), NotNull]

        public int student_teachers_id { get; set; }

        [MaxLength(25)]

        public int student_subject_id { get; set; }

        [MaxLength(50),Unique]

        public string student_macAddress { get; set; }
    }
}