namespace PROG_POE1.Models
{
    public class Claim
    {
        public int Id { get; set; }
        public string TotalHours { get; set; }
        public string HourlyRate { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime DateSubmitted { get; set; }
    }

}
