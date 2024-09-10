using Microsoft.AspNetCore.Mvc;

namespace PROG_POE1.Controllers
{

    
    
        public class AccountController : Controller
        {
            // Simulating a user database
            private static List<(string username, string password)> Users = new List<(string username, string password)>
        {
            ("admin", "password")
        };

            // Display the Login page
            public IActionResult Login()
            {
                return View();
            }

            // Handle the login form submission
            [HttpPost]
            public IActionResult Login(string username, string password)
            {
                // Check username and password (authentication logic)
                var user = Users.Find(u => u.username == username && u.password == password);

                if (user != default)
                {
                    // Placeholder: Perform login logic (e.g., set authentication cookie or session)
                    TempData["Username"] = username; // Temporarily storing username for demonstration

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.ErrorMessage = "Invalid username or password";
                    return View();
                }
            }

            // Handle the logout action
            public IActionResult Logout()
            {
                // Clear the TempData (simulating user logout)
                TempData.Remove("Username");

                return RedirectToAction("Login");
            }
        }
    



}
