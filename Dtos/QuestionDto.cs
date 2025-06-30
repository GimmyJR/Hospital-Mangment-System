namespace Hospital_Mangment_System.Dtos
{
    public class QuestionDto
    {
        public int Id { get; set; }
        public string PatientName { get; set; }
        public string QuestionText { get; set; }
        public string? ImageUrl { get; set; }
        public string? Answer { get; set; }
        public string? DoctorName { get; set; }
        public DateTime AskedAt { get; set; }
    }

}
