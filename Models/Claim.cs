using System.ComponentModel.DataAnnotations;

namespace PROG_POE1.Models
{
    public class Claim
    {
        [Key]
        public int Id { get; set; }
        public string TotalHours { get; set; }
        public string HourlyRate { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime DateSubmitted { get; set; }
        public string SupportingDocument { get; set; }
        public string Comments { get; set; }
        public string SubmittedBy { get; set; } // Store the lecturer's identifier (e.g., email or user ID)
        public string Status { get; set; } = "Pending"; // Default status
        public string? RejectionReason { get; internal set; }
    }


}
