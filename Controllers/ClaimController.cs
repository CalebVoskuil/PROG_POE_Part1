using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PROG_POE1.Data;
using PROG_POE1.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace PROG_POE1.Controllers
{

    public static class ClaimValidationRules
    {
        public const decimal MaxHoursPerMonth = 160; // Example: 4 weeks * 40 hours
        public const decimal MinHourlyRate = 10.0m; // Minimum allowed hourly rate
        public const decimal MaxHourlyRate = 100.0m; // Maximum allowed hourly rate
    }


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
            var submittedBy = User.Identity.Name;

            // Parse input safely
            if (!decimal.TryParse(totalHours, out var hours))
            {
                ModelState.AddModelError("totalHours", "Invalid input for total hours.");
                return View();
            }
            if (!decimal.TryParse(hourlyRate, out var rate))
            {
                ModelState.AddModelError("hourlyRate", "Invalid input for hourly rate.");
                return View();
            }

            // Validate file
            const long MaxFileSize = 5 * 1024 * 1024; // 5MB
            string filePath = null;
            if (supportingDocument == null || supportingDocument.Length == 0)
            {
                ModelState.AddModelError("supportingDocument", "A supporting document is required.");
                return View();
            }
            if (supportingDocument.Length > MaxFileSize)
            {
                ModelState.AddModelError("supportingDocument", "The file size exceeds the 5MB limit.");
                return View();
            }
            var allowedExtensions = new[] { ".pdf", ".docx", ".xlsx" };
            var fileExtension = Path.GetExtension(supportingDocument.FileName).ToLower();
            if (!allowedExtensions.Contains(fileExtension))
            {
                ModelState.AddModelError("supportingDocument", "Invalid file type. Only PDF, DOCX, and XLSX are allowed.");
                return View();
            }

            // Save file
            try
            {
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
            catch (Exception)
            {
                ModelState.AddModelError("supportingDocument", "An error occurred while saving the file. Please try again.");
                return View();
            }

            // Calculate total amount
            var totalAmount = hours * rate;

            // Automated verification
            var status = "Approved";
            string rejectionReason = null;

            if (hours > ClaimValidationRules.MaxHoursPerMonth)
            {
                status = "Pending";
                rejectionReason = $"Hours worked ({hours}) exceed the maximum allowed per month ({ClaimValidationRules.MaxHoursPerMonth}).";
            }
            if (rate < ClaimValidationRules.MinHourlyRate || rate > ClaimValidationRules.MaxHourlyRate)
            {
                status = "Pending";
                rejectionReason = $"Hourly rate ({rate:C}) is outside the allowed range ({ClaimValidationRules.MinHourlyRate:C} - {ClaimValidationRules.MaxHourlyRate:C}).";
            }

            // Save claim
            var newClaim = new Claim
            {
                TotalHours = totalHours,
                HourlyRate = hourlyRate,
                TotalAmount = totalAmount,
                SupportingDocument = filePath,
                Comments = comments,
                SubmittedBy = submittedBy,
                DateSubmitted = DateTime.Now,
                Status = status,
                RejectionReason = rejectionReason
            };
            _context.Claims.Add(newClaim);
            _context.SaveChanges();

            // Notify coordinator for pending claims
            if (status == "Pending")
            {
                // Notify (example code for email)
                TempData["RejectionReason"] = rejectionReason;
                return RedirectToAction("Index", "Home");
            }

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



