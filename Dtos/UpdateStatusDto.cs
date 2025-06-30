using System.ComponentModel.DataAnnotations;

namespace Hospital_Mangment_System.Dtos
{
    // DTOs/UpdateStatusDto.cs
    public class UpdateStatusDto
    {
        [Required]
        public string Status { get; set; } // "Pending", "Confirmed", "Cancelled"
    }
}
