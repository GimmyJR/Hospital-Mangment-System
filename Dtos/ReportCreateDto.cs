namespace Hospital_Mangment_System.Dtos
{
    public class ReportCreateDto
    {
        public string ReportName { get; set; }
        public string ReportUrl { get; set; }
        public string Status { get; set; }
        public string PatientId { get; set; } // Use string, not int
    }



}
