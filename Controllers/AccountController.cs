using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PROG_POE1.ViewModels;


namespace PROG_POE1.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        // Displays the role selection page
        public IActionResult ChooseRole()
        {
            return View();
        }
        public IActionResult LoginSelection()
        {
            return View();
        }


        // Redirects the user to the appropriate login page based on role
        public IActionResult LoginAsLecturer()
        {
            return RedirectToAction("Login", new { role = "Lecturer" });
        }

        public IActionResult LoginAsCoordinator()
        {
            return RedirectToAction("Login", new { role = "Coordinator" });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string role, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    if (await _userManager.IsInRoleAsync(user, role))
                    {
                        if (role == "Lecturer")
                        {
                            return RedirectToAction("Submit", "Claim");
                        }
                        else if (role == "Coordinator")
                        {
                            return RedirectToAction("ReviewClaims", "Claim");
                        }
                    }
                }
            }
            return View(model);
        }
    }

}



