using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.Json.Serialization;

public class AppointmentRequest
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
    [RegularExpression(@"^(0[0-9]|1[0-2]):[0-5][0-9] (AM|PM)$", ErrorMessage = "Time must be in 'HH:MM AM/PM' format")]
    public string DisplayTime { get; set; } // e.g., "10:00 AM"

    [JsonIgnore] // Prevents Swagger from showing this field
    public TimeSpan AppointmentTime => DateTime.ParseExact(DisplayTime, "h:mm tt", CultureInfo.InvariantCulture).TimeOfDay;

    public List<IFormFile> Images { get; set; }
}