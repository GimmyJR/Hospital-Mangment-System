namespace Hospital_Mangment_System.Dtos
{
    public class AddDoctorDto
    {
        public string Email { get; set; }
        public string Specialization { get; set; }
        public string Schedule { get; set; }
        public string AppUserId { get; set; } // ID of the existing AppUser
    }

}
