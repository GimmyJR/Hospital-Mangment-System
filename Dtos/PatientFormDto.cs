namespace Hospital_Mangment_System.Dtos
{
    public class PatientFormDto
    {
        public string PatientName { get; set; }
        public int Age { get; set; }
        public string MedicalHistory { get; set; }
        public string Medications { get; set; }
        public DateTime LastVisit { get; set; }
        public string phone { get; set; }
        public int? FollowUpFrequency { get; set; }
    }
}
