namespace Hospital_Mangment_System.Dtos
{
    public class PatientProfileDto : ProfileDto
    {
        public int Age { get; set; }
        public string MedicalHistory { get; set; }
        public string Medications { get; set; }
        public DateTime? LastVisit { get; set; }
        public int? FollowUpFrequency { get; set; }
    }

}
