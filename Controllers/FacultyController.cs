using Microsoft.AspNetCore.Mvc;
using HiSUP.Data;

namespace HiSUP.Controllers
{
    public class FacultyController : Controller
    {
        public IActionResult Index()
        {
            return View(DatabaseRepository.Assigns);
        }

        [HttpPost]
        public IActionResult GradeStudent(int studentId, string courseCode, string marks, string attendance)
        {
            DatabaseRepository.UpdateStudentPerformance(studentId, courseCode, marks, attendance);
            TempData["SuccessMessage"] = "Student attendance vectors and evaluation grades updated in real-time!";
            return RedirectToAction(nameof(Index));
        }
    }
}
