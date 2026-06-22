using System;

namespace HiSUP.Models
{
    public class Student
    {
        public int StudentID { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int DepartmentID { get; set; }
    }
}
