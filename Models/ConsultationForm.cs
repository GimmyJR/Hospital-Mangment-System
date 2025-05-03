namespace Hospital_Mangment_System.Models
{
    public class ConsultationForm
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime SubmissionDate { get; set; }
        public string PatientId { get; set; }
        public Patient Patient { get; set; }
    }
}
