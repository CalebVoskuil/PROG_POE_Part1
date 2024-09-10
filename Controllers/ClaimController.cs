using Microsoft.AspNetCore.Mvc;

namespace PROG_POE1.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    namespace CMCS.Controllers
    {
        public class ClaimController : Controller
        {
            // Display the Submit Claim form
            public IActionResult Submit()
            {
                return View();
            }

            // Handle form submission of a new claim
            [HttpPost]
            public IActionResult Submit(string totalHours, string hourlyRate, string comments)
            {
                // Placeholder: Handle the claim submission logic (e.g., saving claim to the database)
                // Redirect to Claim History page after submission
                return RedirectToAction("History");
            }

            // View the Claim History
            public IActionResult History()
            {
                // Placeholder: Fetch the submitted claims from the database and pass to the view
                return View();
            }

            // View specific claim details
            public IActionResult Details(int id)
            {
                // Placeholder: Fetch details for a specific claim by its ID
                return View();
            }
        }
    }

}
