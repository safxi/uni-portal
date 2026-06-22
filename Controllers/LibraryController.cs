using Microsoft.AspNetCore.Mvc;
using HiSUP.Data;
using System.Data;

namespace HiSUP.Controllers
{
    public class LibraryController : Controller
    {
        private readonly IDatabaseRepository _repo;
        public LibraryController(IDatabaseRepository repo) => _repo = repo;

        [HttpGet]
        public async Task<IActionResult> Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return View(new DataTable());
            }

            // Calls Stored Procedure with Full-Text Index optimizations
            DataTable results = await _repo.SearchLibraryAdvancedAsync(query);
            return View(results);
        }
    }
}
