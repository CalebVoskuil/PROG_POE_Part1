using Microsoft.AspNetCore.Mvc;
using PROG_POE1.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace PROG_POE1.Controllers
{


    public class ClaimController : Controller
    {
        // Simulating in-memory storage for claims (Replace with a database later)
        private static List<Claim> Claims = new List<Claim>();
        private static int NextId = 1;

        // Display the Submit Claim form
        public IActionResult Submit()
        {
            return View();
        }

        // Handle form submission of a new claim
        [HttpPost]
        public IActionResult Submit(string totalHours, string hourlyRate, string comments)
        {
            // Parse total hours and hourly rate
            decimal hours = decimal.Parse(totalHours);
            decimal rate = decimal.Parse(hourlyRate);

            // Calculate total amount
            decimal totalAmount = hours * rate;

            // Create a new claim object
            Claim newClaim = new Claim
            {
                Id = NextId++,
                TotalHours = totalHours,
                HourlyRate = hourlyRate, 
                TotalAmount = totalAmount,
                DateSubmitted = DateTime.Now
            };

            // Add the new claim to the list (simulating saving to a database)
            Claims.Add(newClaim);

            // Redirect to Claim History page after submission
            return RedirectToAction("History");
        }

        // View the Claim History
        public IActionResult History()
        {
            // Pass the list of claims to the view
            return View(Claims);
        }

        // View specific claim details
        public IActionResult Details(int id)
        {
            // Find the claim by ID
            var claim = Claims.FirstOrDefault(c => c.Id == id);
            if (claim == null)
            {
                return NotFound();
            }

            return View(claim);
        }
    }
}
        


    
