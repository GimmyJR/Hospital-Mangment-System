namespace Hospital_Mangment_System.Models
{
    public class Report
    {
        public int Id { get; set; }
        public string ReportName { get; set; }
        public string ReportUrl { get; set; }
        public string Status { get; set; } // e.g., "Downloaded", "On Hold"
        public DateTime CreatedAt { get; set; }

        // Foreign Key
        public string PatientId { get; set; }
        public AppUser Patient { get; set; }
    }



}
