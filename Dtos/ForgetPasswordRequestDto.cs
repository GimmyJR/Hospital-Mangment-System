using System.ComponentModel.DataAnnotations;

namespace Hospital_Mangment_System.Dtos
{
    public class ForgetPasswordRequestDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
