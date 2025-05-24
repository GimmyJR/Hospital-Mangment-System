namespace Hospital_Mangment_System.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string MedicalHistory { get; set; }
        public AppUser appUser { get; set; }
        public string AppUserId { get; set; }
        public int age { get; set; }
        public string? Medications { get; set; }
        public DateTime? LastVisit { get; set; } = DateTime.UtcNow;
        public int? FollowUpFrequency { get; set; } 
        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<ConsultationForm> ConsultationForms { get; set; }
    }
}
