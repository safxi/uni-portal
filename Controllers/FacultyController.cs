using Microsoft.AspNetCore.Mvc;
using HiSUP.Data;

namespace HiSUP.Controllers
{
    public class FacultyController : Controller
    {
        public IActionResult Index() => View(DatabaseRepository.Assigns);

        [HttpPost]
        public IActionResult GradeStudent(int studentId, string courseCode, string s1, string s2, string quizzes, string finalExam, string attendance)
        {
            DatabaseRepository.UpdateHitecPerformance(studentId, courseCode, s1, s2, quizzes, finalExam, attendance);
            TempData["SuccessMessage"] = "HITEC Relative Grading Parameters sync successfully updated!";
            return RedirectToAction(nameof(Index));
        }
    }
}
