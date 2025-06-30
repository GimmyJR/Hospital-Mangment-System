namespace Hospital_Mangment_System.Dtos
{
    public class DoctorDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Specialization { get; set; }
        public string Schedule { get; set; }
        public string FullName { get; set; } // from AppUser
    }
}
