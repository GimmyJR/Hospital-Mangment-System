using System.ComponentModel.DataAnnotations;

namespace Hospital_Mangment_System.Dtos
{
    // DTOs/AppointmentCreateDto.cs
    public class AppointmentCreateDto
    {
        [Required]
        public string PatientName { get; set; }

        [Required]
        public string PatientId { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public int DoctorId { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; }

        [Required]
        public TimeSpan AppointmentTime { get; set; }
    }
}
