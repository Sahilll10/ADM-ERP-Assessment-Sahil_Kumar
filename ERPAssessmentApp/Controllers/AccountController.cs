using Microsoft.AspNetCore.Mvc;
using ERPAssessmentApp.Models;
using System.Linq;

namespace ERPAssessmentApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.PasswordHash == password);

            if (user != null)
            {
                return RedirectToAction("Index", "Item");
            }

            ViewBag.Error = "Invalid email or password.";
            return View();
        }

        public IActionResult Logout()
        {
            return RedirectToAction("Login");
        }
    }
}