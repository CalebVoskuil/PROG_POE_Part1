using Microsoft.AspNetCore.Mvc;
using PROG_POE1.Data;
using PROG_POE1.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace PROG_POE1.Controllers
{

    

    public class ClaimController : Controller
    {
        private readonly AppDbContext _context;

        public ClaimController(AppDbContext context)
        {
            _context = context;
        }

        // Simulating in-memory storage for claims (Replace with a database later)
        private static List<Claim> Claims = new List<Claim>();
        

        // Display the Submit Claim form
        public IActionResult Submit()
        {
            return View();
        }

        // Handle form submission of a new claim
        [HttpPost]
        public IActionResult Submit(string totalHours, string hourlyRate, string comments, IFormFile supportingDocument)
        {
            // Parse total hours and hourly rate
            decimal hours = decimal.Parse(totalHours);
            decimal rate = decimal.Parse(hourlyRate);

            // Calculate total amount
            decimal totalAmount = hours * rate;
            string filePath = null;
            if (supportingDocument != null && supportingDocument.Length > 0)
            {
                var fileExtension = Path.GetExtension(supportingDocument.FileName).ToLower();
                var allowedExtensions = new[] { ".pdf", ".docx", ".xlsx" };
                if (!allowedExtensions.Contains(fileExtension))
                {
                    ModelState.AddModelError("supportingDocument", "Invalid file type. Only PDF, DOCX, and XLSX are allowed.");
                    return View();
                }

                // Save the file to a designated folder, e.g., "wwwroot/uploads"
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                filePath = Path.Combine(uploadsFolder, supportingDocument.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    supportingDocument.CopyTo(stream);
                }
            }
            // Create a new claim object
            Claim newClaim = new Claim
            {
                
                TotalHours = totalHours,
                HourlyRate = hourlyRate, 
                TotalAmount = totalAmount,
                SupportingDocument = filePath,
                Comments = comments,
                DateSubmitted = DateTime.Now

            };

            // Add the new claim to the database
            _context.Claims.Add(newClaim);
            _context.SaveChanges(); // Save the changes to the database

            // Redirect to Claim History page after submission
            return RedirectToAction("Index", "Home");
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
// ~~~~~~~~^^~~^^~~^^~[ End of File ]~^^~~^^~~^^~~~~~~~~



