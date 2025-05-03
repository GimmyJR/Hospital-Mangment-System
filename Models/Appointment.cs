namespace Hospital_Mangment_System.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public string Status { get; set; }

        public string PatientId { get; set; }
        public Patient Patient { get; set; }

        public string DoctorId { get; set; }
        public Doctor Doctor { get; set; }
    }
}
