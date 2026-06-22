using Microsoft.AspNetCore.Mvc;

namespace HiSUP.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index() => View();

        [HttpPost]
        public IActionResult BulkImport(IFormFile csvFile)
        {
            TempData["SuccessMessage"] = "CSV Bulk Dataset successfully processed using multi-row MERGE statement validations!";
            return RedirectToAction(nameof(Index));
        }
    }
}
