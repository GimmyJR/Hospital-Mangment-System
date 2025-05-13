using System.ComponentModel.DataAnnotations;

namespace Hospital_Mangment_System.Dtos
{
    public class CreateQuestionDto
    {
        [Required]
        public string QuestionText { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
