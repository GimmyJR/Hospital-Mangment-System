namespace Hospital_Mangment_System.Dtos
{
    public class PatientDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MedicalHistory { get; set; }
        public int Age { get; set; }
        public string Medications { get; set; }
        public DateTime? LastVisit { get; set; }
        public int? FollowUpFrequency { get; set; }
    }
}
