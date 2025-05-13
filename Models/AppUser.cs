using Microsoft.AspNetCore.Identity;

namespace Hospital_Mangment_System.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
        public string phone { get; set; }

        public string? ResetPasswordOTP { get; set; }
        public DateTime? ResetPasswordOTPExpiry { get; set; }
        public bool? IsResetPasswordOTPUsed { get; set; }

    }
}
