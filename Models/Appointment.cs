using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Hospital_Mangment_System.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        [Required]
        public string PatientName { get; set; }

        [Required]
        public string PatientId { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public int DoctorId { get; set; }
        [JsonIgnore]
        public Doctor Doctor { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; }

        [Required]
        public TimeSpan AppointmentTime { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = "Pending"; // Pending, Confirmed, Cancelled

        public ICollection<MedicalImage> MedicalImages { get; set; }
    }
}
