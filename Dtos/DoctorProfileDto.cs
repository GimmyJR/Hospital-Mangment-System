namespace Hospital_Mangment_System.Dtos
{
    public class DoctorProfileDto : ProfileDto
    {
        public string Email { get; set; }
        public string Specialization { get; set; }
        public string Schedule { get; set; }
    }

}
