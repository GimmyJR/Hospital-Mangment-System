using Hospital_Mangment_System.Dtos;
using Hospital_Mangment_System.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Hospital_Mangment_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PatientsController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPatients() =>
        Ok(await _context.Patients.ToListAsync());

        [HttpPost]
        public async Task<IActionResult> AddPatient(Patient patient)
        {
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
            return Ok(patient);
        }

        // PatientsController.cs
        [HttpGet("patients")]
        public async Task<IActionResult> GetPatients([FromQuery] PatientQueryParams queryParams)
        {
            var query = _context.Patients
                .Include(p => p.appUser)
                .AsQueryable();

            // Filtering
            if (!string.IsNullOrEmpty(queryParams.Search))
            {
                query = query.Where(p =>
                    p.appUser.FullName.Contains(queryParams.Search) ||
                    p.appUser.PhoneNumber.Contains(queryParams.Search));
            }

            // Sorting
            query = queryParams.SortBy switch
            {
                "name" => queryParams.SortOrder == "desc"
                    ? query.OrderByDescending(p => p.appUser.FullName)
                    : query.OrderBy(p => p.appUser.FullName),
                "date" => queryParams.SortOrder == "desc"
                    ? query.OrderByDescending(p => p.LastVisit)
                    : query.OrderBy(p => p.LastVisit),
                _ => query.OrderBy(p => p.Id)
            };

            // Pagination
            var totalCount = await query.CountAsync();
            var patients = await query
                .Skip((queryParams.PageNumber - 1) * queryParams.PageSize)
                .Take(queryParams.PageSize)
                .Select(p => new PatientListDto
                {
                    Id = p.Id,
                    Name = p.appUser.FullName,
                    Email = p.appUser.Email,
                    Phone = p.appUser.PhoneNumber,
                    Age = p.age,
                    MedicalCondition = p.MedicalHistory,
                    LastVisit = p.LastVisit
                })
                .ToListAsync();

            var paginationHeader = new
            {
                totalCount,
                pageSize = queryParams.PageSize,
                currentPage = queryParams.PageNumber,
                totalPages = (int)Math.Ceiling(totalCount / (double)queryParams.PageSize)
            };

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationHeader));

            return Ok(patients);
        }
    }
}
