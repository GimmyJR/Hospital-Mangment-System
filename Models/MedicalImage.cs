using System.Text.Json.Serialization;

namespace Hospital_Mangment_System.Models
{
    public class MedicalImage
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public int AppointmentId { get; set; }
        [JsonIgnore]
        public Appointment Appointment { get; set; }
    }
}
