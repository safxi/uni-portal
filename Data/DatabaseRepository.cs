using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HiSUP.Data
{
    public class DatabaseRepository : IDatabaseRepository
    {
        // HITEC Standard Matrix In-Memory Database Engine
        public static List<Dictionary<string, string>> Students = new List<Dictionary<string, string>>
        {
            new Dictionary<string, string> { {"ID", "1"}, {"Name", "Saif Ur Rehman"}, {"Email", "saif@hitec.edu.pk"}, {"GPA", "3.84"} },
            new Dictionary<string, string> { {"ID", "2"}, {"Name", "Ali Ahmed"}, {"Email", "ali@hitec.edu.pk"}, {"GPA", "3.21"} }
        };

        public static List<Dictionary<string, string>> Assigns = new List<Dictionary<string, string>>
        {
            new Dictionary<string, string> { 
                {"CourseCode", "SE-402"}, {"CourseTitle", "Software Automated Testing"}, {"Faculty", "Dr. Asim"}, {"StudentID", "1"}, 
                {"Sessional1", "12"}, {"Sessional2", "11"}, {"Quizzes", "17"}, {"FinalExam", "34"}, {"Attendance", "94.5"} 
            },
            new Dictionary<string, string> { 
                {"CourseCode", "CS-412"}, {"CourseTitle", "Mobile App Development"}, {"Faculty", "Dr. Asim"}, {"StudentID", "1"}, 
                {"Sessional1", "14"}, {"Sessional2", "13"}, {"Quizzes", "18"}, {"FinalExam", "41"}, {"Attendance", "71.2"} // Trigger Detained Warning
            }
        };

        public static List<string> GlobalCourses = new List<string> { "Software Automated Testing", "Mobile App Development", "Advanced Databases", "Web Frameworks" };
        public static List<string> GlobalFaculty = new List<string> { "Dr. Asim", "Prof. Kamran", "Engr. Sana" };

        public async Task<int> RegisterStudentAsync(string firstName, string lastName, string email, int deptId)
        {
            int newId = Students.Count + 1;
            Students.Add(new Dictionary<string, string> {
                { "ID", newId.ToString() }, { "Name", $"{firstName} {lastName}" }, { "Email", email }, { "GPA", "0.0" }
            });
            return await Task.FromResult(newId);
        }

        public async Task<bool> AssignCourseToFacultyAndStudent(string course, string faculty, int studentId)
        {
            Assigns.Add(new Dictionary<string, string> {
                { "CourseCode", "CS-" + new Random().Next(100, 499) }, { "CourseTitle", course }, { "Faculty", faculty }, { "StudentID", studentId.ToString() },
                { "Sessional1", "0" }, { "Sessional2", "0" }, { "Quizzes", "0" }, { "FinalExam", "0" }, { "Attendance", "100" }
            });
            return await Task.FromResult(true);
        }

        public async Task<bool> EnrollInCourseAsync(int studentId, int sectionId) => await Task.FromResult(true);
        public async Task<DataTable> SearchLibraryAdvancedAsync(string searchString) => await Task.FromResult(new DataTable());
        public async Task<bool> ProcessFeePaymentAsync(int studentId, decimal amount, string paymentMode) => await Task.FromResult(true);

        public async Task<DataTable> GetStudentDashboardDataAsync(int studentId)
        {
            var dt = new DataTable();
            dt.Columns.Add("StudentID"); dt.Columns.Add("FirstName"); dt.Columns.Add("LastName"); dt.Columns.Add("Email"); dt.Columns.Add("GPA"); dt.Columns.Add("AttendancePercentage");
            var std = Students.FirstOrDefault(s => s["ID"] == studentId.ToString()) ?? Students[0];
            var names = std["Name"].Split(' ');
            dt.Rows.Add(std["ID"], names[0], names.Length > 1 ? names[1] : "", std["Email"], std["GPA"], "94.5");
            return await Task.FromResult(dt);
        }

        public async Task<DataTable> GetStudentAcademicGradesAsync(int studentId)
        {
            var dt = new DataTable();
            dt.Columns.Add("CourseCode"); dt.Columns.Add("CourseTitle"); dt.Columns.Add("Faculty"); 
            dt.Columns.Add("Sessional1"); dt.Columns.Add("Sessional2"); dt.Columns.Add("Quizzes"); dt.Columns.Add("FinalExam");
            dt.Columns.Add("TotalMarks"); dt.Columns.Add("Attendance");

            var matches = Assigns.Where(a => a["StudentID"] == studentId.ToString());
            foreach (var m in matches)
            {
                int total = int.Parse(m["Sessional1"]) + int.Parse(m["Sessional2"]) + int.Parse(m["Quizzes"]) + int.Parse(m["FinalExam"]);
                dt.Rows.Add(m["CourseCode"], m["CourseTitle"], m["Faculty"], m["Sessional1"], m["Sessional2"], m["Quizzes"], m["FinalExam"], total.ToString(), m["Attendance"]);
            }
            return await Task.FromResult(dt);
        }

        public static void UpdateHitecPerformance(int studentId, string courseCode, string s1, string s2, string qz, string fn, string attendance)
        {
            var match = Assigns.FirstOrDefault(a => a["StudentID"] == studentId.ToString() && a["CourseCode"] == courseCode);
            if (match != null)
            {
                match["Sessional1"] = s1;
                match["Sessional2"] = s2;
                match["Quizzes"] = qz;
                match["FinalExam"] = fn;
                match["Attendance"] = attendance;
            }
        }
    }
}
