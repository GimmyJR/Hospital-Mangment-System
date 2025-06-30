using System.ComponentModel.DataAnnotations;

namespace Hospital_Mangment_System.Dtos
{
    public class PatientListDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string Phone { get; set; }

        [Range(0, 120)]
        public int Age { get; set; }

        [StringLength(500)]
        public string MedicalCondition { get; set; }

        [DataType(DataType.Date)]
        public DateTime? LastVisit { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }

        public string BloodGroup { get; set; }

        public string Gender { get; set; }

        public bool HasInsurance { get; set; }
    }
}
