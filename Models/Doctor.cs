namespace Hospital_Mangment_System.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Specialization { get; set; }
        public string Schedule { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        public AppUser appUser { get; set; }
        public string AppUserId { get; set; }
    }
}
