using Microsoft.AspNetCore.Mvc;
using HiSUP.Data;
using System.Data;
using System.Threading.Tasks;

namespace HiSUP.Controllers
{
    public class StudentController : Controller
    {
        private readonly IDatabaseRepository _repo;
        public StudentController(IDatabaseRepository repo) => _repo = repo;

        public async Task<IActionResult> Dashboard()
        {
            int currentStudentId = 1;
            DataTable studentInfoView = await _repo.GetStudentDashboardDataAsync(currentStudentId);
            return View(studentInfoView);
        }

        [HttpGet]
        public IActionResult RegisterCourse() => View();

        [HttpPost]
        public async Task<IActionResult> RegisterCourse(int sectionId)
        {
            int currentStudentId = 1;
            bool success = await _repo.EnrollInCourseAsync(currentStudentId, sectionId);
            if (success)
            {
                TempData["SuccessMessage"] = "Course enrollment token successfully generated via Atomic DB Isolation!";
                return RedirectToAction(nameof(Dashboard));
            }
            ModelState.AddModelError("", "Enrollment error or concurrency limit hit.");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PayFees(decimal amount, string paymentMode)
        {
            int currentStudentId = 1;
            bool success = await _repo.ProcessFeePaymentAsync(currentStudentId, amount, paymentMode);
            if (success)
            {
                TempData["SuccessMessage"] = $"Tuition settlement of PKR {amount} cleared successfully via secure pipeline!";
            }
            return RedirectToAction(nameof(Dashboard));
        }
    }
}
