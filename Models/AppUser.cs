using Microsoft.AspNetCore.Identity;

namespace Hospital_Mangment_System.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
        public string phone { get; set; }
    }
}
