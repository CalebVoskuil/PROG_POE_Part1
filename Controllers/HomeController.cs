using Microsoft.AspNetCore.Mvc;
using PROG_POE1.Models;
using System.Diagnostics;

namespace PROG_POE1.Controllers
{
    
    
        public class HomeController : Controller
        {
            public IActionResult Index()
            {
                return View();
            }

            public IActionResult About()
            {
                ViewBag.Message = "Your application description page.";
                return View();
            }

            public IActionResult Contact()
            {
                ViewBag.Message = "Your contact page.";
                return View();
            }
        }
    



}
