using Hospital_Mangment_System.Dtos;
using Hospital_Mangment_System.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Mangment_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("patient-summary-by-id/{patientId}")]
        public async Task<IActionResult> GetPatientSummaryById(int patientId)
        {
            var patient = await _context.Patients
                .Include(p => p.Appointments)
                .Include(p => p.ConsultationForms)
                .FirstOrDefaultAsync(p => p.Id == patientId);

            if (patient == null)
                return NotFound("المريض غير موجود");

            var dto = new PatientDashboardDto
            {
                Age = patient.age,
                Medications = patient.Medications,
                LastVisit = patient.LastVisit,
                AppointmentCount = patient.Appointments.Count,
                ConsultationCount = patient.ConsultationForms.Count,
                FollowUpFrequency = patient.FollowUpFrequency
            };

            return Ok(dto);
        }

        // DashboardController.cs
        [HttpGet("dashboard/stats")]
        public async Task<IActionResult> GetDashboardStats()
        {
            var stats = new
            {
                Doctors = await _context.Doctors.CountAsync(),
                Patients = await _context.Patients.CountAsync(),
                Appointments = await _context.Appointments.CountAsync(a => a.AppointmentDate >= DateTime.Today),
                NewPatients = await _context.Patients
                    .CountAsync(p => p.LastVisit >= DateTime.Today.AddDays(-30))
            };

            return Ok(stats);
        }

    }
}
