using Hospital_Mangment_System.Dtos;
using Hospital_Mangment_System.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Mangment_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultationsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ConsultationsController(AppDbContext context)
        {
            _context = context;
        }

        
        [HttpPost("PostConsultation")]
        public async Task<ActionResult<ConsultationForm>> PostConsultation(ConsultationDto consultationDto)
        {
            var consultation = new ConsultationForm
            {
                PatientName = consultationDto.PatientName,
                PatientId = consultationDto.PatientId,
                Symptoms = consultationDto.Symptoms,
                DoctorName = consultationDto.DoctorName,
                SubmissionDate = DateTime.UtcNow,
                Status = "Pending"
            };

            _context.ConsultationForms.Add(consultation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetConsultation", new { id = consultation.Id }, consultation);
        }

        
        [HttpGet("GetConsultation/{id}")]
        public async Task<ActionResult<ConsultationForm>> GetConsultation(int id)
        {
            var consultation = await _context.ConsultationForms.FindAsync(id);

            if (consultation == null)
            {
                return NotFound();
            }

            return consultation;
        }
    }
}

