namespace Hospital_Mangment_System.Dtos
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        public string PatientName { get; set; }
        public string PatientId { get; set; }
        public string PhoneNumber { get; set; }
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string Specialization { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string FormattedTime { get; set; } // e.g., "10:00 AM"
        public string Status { get; set; }
        public List<string> MedicalImageUrls { get; set; } = new();
    }
}
