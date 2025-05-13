using System.ComponentModel.DataAnnotations;

namespace Hospital_Mangment_System.Models
{
    public class Question
    {
        public int Id { get; set; }

        [Required]
        public string PatientId { get; set; }

        [Required]
        public string PatientName { get; set; }

        [Required]
        public string QuestionText { get; set; }
        public string? ImagePath { get; set; }
        public DateTime AskedAt { get; set; } = DateTime.UtcNow;
        public string? Answer { get; set; }
        public DateTime? AnsweredAt { get; set; }
        public int? DoctorId { get; set; }
        public Doctor? Doctor { get; set; }
    }
}
