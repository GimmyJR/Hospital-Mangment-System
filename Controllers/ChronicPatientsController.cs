using Hospital_Mangment_System.Dtos;
using Hospital_Mangment_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Hospital_Mangment_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChronicPatientsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ChronicPatientsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("CreatePatient")]
        public async Task<ActionResult<Patient>> CreatePatient([FromBody] PatientFormDto patientDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _context.Patients.FirstOrDefault(u => u.appUser.FullName == patientDto.PatientName);
            if (user == null)
            {
                return NotFound("Full Name is Wrong!");
            }

            user.MedicalHistory = patientDto.MedicalHistory;
            user.age = patientDto.Age;
            user.Medications = patientDto.Medications;
            user.LastVisit = patientDto.LastVisit;
            user.FollowUpFrequency = patientDto.FollowUpFrequency;
            
            
            await _context.SaveChangesAsync();

            return Ok("Done");
        }

        
        [HttpGet("GetAllPatients")]
        public async Task<ActionResult<IEnumerable<PatientDto>>> GetAllPatients()
        {
            var patients = await _context.Patients
                .Include(p => p.appUser)
                .Select(p => new PatientDto
                {
                    Id = p.Id,
                    Name = p.appUser != null ? $"{p.appUser.FullName}" : "Unknown",
                    MedicalHistory = p.MedicalHistory,
                    Age = p.age,
                    Medications = p.Medications,
                    LastVisit = p.LastVisit,
                    FollowUpFrequency = p.FollowUpFrequency
                })
                .ToListAsync();

            return Ok(patients);
        }

        [HttpGet("GetPatientById/{id}")]
        public async Task<ActionResult<PatientDto>> GetPatientById(int id)
        {
            var patient = await _context.Patients
                .Include(p => p.appUser)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (patient == null)
            {
                return NotFound();
            }

            var patientDto = new PatientDto
            {
                Id = patient.Id,
                Name = patient.appUser != null ? $"{patient.appUser.FullName}" : "Unknown",
                MedicalHistory = patient.MedicalHistory,
                Age = patient.age,
                Medications = patient.Medications,
                LastVisit = patient.LastVisit,
                FollowUpFrequency = patient.FollowUpFrequency
            };

            return Ok(patientDto);
        }


    }
}
