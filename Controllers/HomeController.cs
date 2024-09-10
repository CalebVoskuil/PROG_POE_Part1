using Microsoft.AspNetCore.Mvc;
using PROG_POE1.Models;
using System.Diagnostics;

namespace PROG_POE1.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }

    public class ClaimController : Controller
    {
        public IActionResult Submit()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Submit(string totalHours, string hourlyRate, string comments)
        {
            // Placeholder: Save claim data
            return RedirectToAction("History");
        }

        public IActionResult History()
        {
            return View();
        }
    }

    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            // Placeholder: Perform login logic
            return RedirectToAction("Index", "Home");
        }
    }

}
