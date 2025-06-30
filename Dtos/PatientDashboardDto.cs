namespace Hospital_Mangment_System.Dtos
{
    public class PatientDashboardDto
    {
        public int Age { get; set; }
        public string Medications { get; set; }
        public DateTime? LastVisit { get; set; }
        public int AppointmentCount { get; set; }
        public int ConsultationCount { get; set; }
        public int? FollowUpFrequency { get; set; }
    }
}
