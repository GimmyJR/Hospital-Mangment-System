using Hospital_Mangment_System.Dtos;
using Hospital_Mangment_System.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Mangment_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReportController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var reports = await _context.reports
                .Include(r => r.Patient)
                .ToListAsync();
            return Ok(reports);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var report = await _context.reports
                .Include(r => r.Patient)
                .FirstOrDefaultAsync(r => r.Id == id);
            if (report == null) return NotFound();
            return Ok(report);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ReportCreateDto dto)
        {
            var patientExists = await _context.Users.AnyAsync(u => u.Id == dto.PatientId);
            if (!patientExists)
            {
                return BadRequest("Invalid PatientId: no such user found.");
            }

            var report = new Report
            {
                ReportName = dto.ReportName,
                ReportUrl = dto.ReportUrl,
                Status = dto.Status,
                PatientId = dto.PatientId,
                CreatedAt = DateTime.UtcNow
            };

            _context.reports.Add(report);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = report.Id }, report);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ReportUpdateDto dto)
        {
            var report = await _context.reports.FindAsync(id);
            if (report == null) return NotFound();

            report.ReportName = dto.ReportName;
            report.ReportUrl = dto.ReportUrl;
            report.Status = dto.Status;
            report.PatientId = dto.PatientId.ToString();

            await _context.SaveChangesAsync();
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var report = await _context.reports.FindAsync(id);
            if (report == null) return NotFound();

            _context.reports.Remove(report);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

}
