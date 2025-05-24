namespace Hospital_Mangment_System.Models
{
    public class MedicationQuestion
    {
        public int Id { get; set; }
        public string PatientId { get; set; }
        public string PatientName { get; set; }
        public string MedicationType { get; set; }
        public string QuestionText { get; set; }
        public DateTime AskedAt { get; set; }
        public string Status { get; set; } 
        public string AnswerText { get; set; }
        public DateTime? AnsweredAt { get; set; }
    }
}
