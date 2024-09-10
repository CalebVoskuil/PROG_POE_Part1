using Microsoft.AspNetCore.Mvc;

namespace PROG_POE1.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    namespace CMCS.Controllers
    {
        public class AccountController : Controller
        {
            // Display the Login page
            public IActionResult Login()
            {
                return View();
            }

            // Handle the login form submission
            [HttpPost]
            public IActionResult Login(string username, string password)
            {
                // Placeholder: Check username and password (authentication logic)
                // Redirect to home page on successful login, or show an error message on failure
                bool isAuthenticated = (username == "admin" && password == "password"); // Simplified example

                if (isAuthenticated)
                {
                    // Placeholder: Perform login logic (e.g., set authentication cookie or session)
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
                // Placeholder: Perform logout logic (e.g., clear authentication cookie or session)
                return RedirectToAction("Login");
            }
        }
    }

}
