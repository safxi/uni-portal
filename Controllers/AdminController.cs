using Microsoft.AspNetCore.Mvc;
using HiSUP.Data;
using System.Threading.Tasks;

namespace HiSUP.Controllers
{
    public class AdminController : Controller
    {
        private readonly IDatabaseRepository _repo;
        public AdminController(IDatabaseRepository repo) => _repo = repo;

        public IActionResult Index() => View();

        [HttpPost]
        public async Task<IActionResult> CreateStudent(string firstName, string lastName, string email)
        {
            await _repo.RegisterStudentAsync(firstName, lastName, email, 1);
            TempData["SuccessMessage"] = $"Student {firstName} registered successfully in central system registry!";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> AssignCourse(string course, string faculty, int studentId)
        {
            var repoImpl = _repo as DatabaseRepository;
            await repoImpl.AssignCourseToFacultyAndStudent(course, faculty, studentId);
            TempData["SuccessMessage"] = "Course map and teacher allocation dispatched successfully!";
            return RedirectToAction(nameof(Index));
        }
    }
}
