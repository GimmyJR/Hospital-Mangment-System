namespace Hospital_Mangment_System.Models
{
    public class Admin
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public AppUser appUser { get; set; }
        public string AppUserId { get; set; }
    }
}
