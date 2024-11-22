using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PROG_POE1.Data;
using PROG_POE1.Models;
using System.Diagnostics;

namespace PROG_POE1.Controllers
{
    
    
        public class HomeController : Controller
        {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
            {
                return View();
            }
        public IActionResult History()
        {
            
            var userId = User.Identity.Name; 
            var lecturerClaims = _context.Claims.Where(c => c.SubmittedBy == userId).ToList(); 

            return View(lecturerClaims);
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
