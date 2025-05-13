using Hospital_Mangment_System.Dtos;
using Hospital_Mangment_System.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Hospital_Mangment_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public QuestionsController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpPost("CreateQuestion")]
        public async Task<IActionResult> CreateQuestion([FromForm] CreateQuestionDto dto)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(token) as JwtSecurityToken;

            if (jwtToken == null)
                return Unauthorized();

            var userId = jwtToken.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;

            var user = _context.Users.FirstOrDefault(u => u.Email == userId);

            if (user == null)
            {
                return Unauthorized();
            }

            var question = new Question
            {
                PatientId = userId,
                PatientName = user.FullName,
                QuestionText = dto.QuestionText,
                AskedAt = DateTime.UtcNow
            };

            // Handle image upload
            if (dto.ImageFile != null && dto.ImageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads", "questions");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var uniqueFileName = $"{Guid.NewGuid()}_{dto.ImageFile.FileName}";
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.ImageFile.CopyToAsync(stream);
                }

                question.ImagePath = $"/uploads/questions/{uniqueFileName}";
            }

            _context.questions.Add(question);
            await _context.SaveChangesAsync();

            return Ok(new { message = "تم إرسال السؤال بنجاح" });
        }

        [HttpGet("GetAllQuestions")]
        public IActionResult GetAllQuestions()
        {
            var questions = _context.questions
                .OrderByDescending(q => q.AskedAt)
                .Select(q => new
                {
                    q.Id,
                    q.PatientId,
                    q.PatientName,
                    q.QuestionText,
                    q.ImagePath,
                    AskedAt = q.AskedAt.ToString("yyyy-MM-ddTHH:mm:ss")
                })
                .ToList();

            return Ok(questions);
        }

        [HttpGet("GetQuestionById/{id}")]
        public IActionResult GetQuestionById(int id)
        {
            var question = _context.questions
                .Where(q => q.Id == id)
                .Select(q => new
                {
                    q.Id,
                    q.PatientId,
                    q.PatientName,
                    q.QuestionText,
                    q.ImagePath,
                    AskedAt = q.AskedAt.ToString("yyyy-MM-ddTHH:mm:ss")
                })
                .FirstOrDefault();

            if (question == null)
            {
                return NotFound(new { message = "السؤال غير موجود" });
            }

            return Ok(question);
        }

    }
}
