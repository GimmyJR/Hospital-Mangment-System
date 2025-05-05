namespace Hospital_Mangment_System.Models
{
    public class ConsultationForm
    {
        public int Id { get; set; }
        public string PatientName { get; set; }
        public string PatientId { get; set; }
        public string Symptoms { get; set; } 
        public string DoctorName { get; set; }
        public DateTime SubmissionDate { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = "Pending"; 
    }
}
