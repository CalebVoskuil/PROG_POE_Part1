using Microsoft.AspNetCore.Authorization;
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
        public IActionResult Submit(string totalHours, string hourlyRate, string comments, string totalAmount, IFormFile supportingDocument)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Validate and parse inputs
            if (!decimal.TryParse(totalHours, out decimal hours) || hours <= 0 ||
                !decimal.TryParse(hourlyRate, out decimal rate) || rate <= 0 ||
                !decimal.TryParse(totalAmount, out decimal amount) || amount <= 0)
            {
                ModelState.AddModelError("", "Invalid input. Please ensure all values are correct.");
                return View();
            }

            string filePath = null;
            if (supportingDocument != null && supportingDocument.Length > 0)
            {
                var fileExtension = Path.GetExtension(supportingDocument.FileName).ToLower();
                var allowedExtensions = new[] { ".pdf", ".docx", ".xlsx" };
                if (!allowedExtensions.Contains(fileExtension))
                {
                    ModelState.AddModelError("SupportingDocument", "Invalid file type. Only PDF, DOCX, and XLSX files are allowed.");
                    return View();
                }

                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                filePath = Path.Combine(uploadsFolder, $"{Guid.NewGuid()}{fileExtension}");
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    supportingDocument.CopyTo(stream);
                }
            }

            // Create and save the new claim
            var newClaim = new Claim
            {
                TotalHours = totalHours,
                HourlyRate = hourlyRate,
                TotalAmount = amount,
                SupportingDocument = filePath,
                Comments = comments,
                DateSubmitted = DateTime.Now
            };


            // Add the new claim to the database
            _context.Claims.Add(newClaim);
            _context.SaveChanges(); // Save the changes to the database
            TempData["Message"] = "Claim submitted successfully!";

            // Redirect to home page after submission
            return RedirectToAction("Index", "Home");
        }

        // View the Claim History
        public IActionResult History()
        {
            // Retrieve all claims from the database and pass them to the view
            var claims = _context.Claims.ToList();
            return View(claims);
        }

        [Authorize(Roles = "Coordinator")]
        // View pending claims for coordinators and managers
        public IActionResult ReviewClaims()
        {
            // Get all pending claims
            var pendingClaims = _context.Claims.Where(c => c.Status == "Pending").ToList();
            return View("ReviewClaims", pendingClaims);
        }

        [Authorize(Roles = "Coordinator")]
        // Approve a claim
        [HttpPost]
        public IActionResult ApproveClaim(int id)
        {
            var claim = _context.Claims.FirstOrDefault(c => c.Id == id);
            if (claim != null)
            {
                claim.Status = "Approved";
                _context.SaveChanges();
            }
            return RedirectToAction("ReviewClaims");
        }

        [Authorize(Roles = "Coordinator")]
        // Reject a claim
        [HttpPost]
        public IActionResult RejectClaim(int id)
        {
            var claim = _context.Claims.FirstOrDefault(c => c.Id == id);
            if (claim != null)
            {
                claim.Status = "Rejected";
                _context.SaveChanges();
            }
            return RedirectToAction("ReviewClaims");
        }

        // View specific claim details
        public IActionResult Details(int id)
        {
            // Find the claim by ID in the database
            var claim = _context.Claims.FirstOrDefault(c => c.Id == id);
            if (claim == null)
            {
                return NotFound();
            }

            return View(claim);
        }
    }
}
// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~^^~~^^~~^^~[ End of File ]~^^~~^^~~^^~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~



