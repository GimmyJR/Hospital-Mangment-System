using Hospital_Mangment_System.Dtos;
using Hospital_Mangment_System.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Mangment_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DepartmentController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Department
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var departments = await _context.departments.ToListAsync();
            return Ok(departments);
        }

        // GET: api/Department/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var department = await _context.departments.FindAsync(id);
            if (department == null) return NotFound();
            return Ok(department);
        }

        // POST: api/Department
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DepartmentCreateDto dto)
        {
            var department = new Department
            {
                Name = dto.Name,
                Description = dto.Description
            };

            _context.departments.Add(department);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = department.Id }, department);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] DepartmentCreateDto dto)
        {
            var department = await _context.departments.FindAsync(id);
            if (department == null)
                return NotFound();

            department.Name = dto.Name;
            department.Description = dto.Description;

            await _context.SaveChangesAsync();
            return NoContent();
        }


        // DELETE: api/Department/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var department = await _context.departments.FindAsync(id);
            if (department == null) return NotFound();

            _context.departments.Remove(department);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

}
