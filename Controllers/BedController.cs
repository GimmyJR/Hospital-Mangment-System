using Hospital_Mangment_System.Dtos;
using Hospital_Mangment_System.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Mangment_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BedController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BedController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var beds = await _context.beds
                .Include(b => b.Department)
                .ToListAsync();
            return Ok(beds);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var bed = await _context.beds
                .Include(b => b.Department)
                .FirstOrDefaultAsync(b => b.Id == id);
            if (bed == null) return NotFound();
            return Ok(bed);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BedCreateDto dto)
        {
            var bed = new Bed
            {
                BedNumber = dto.BedNumber,
                IsOccupied = dto.IsOccupied,
                DepartmentId = dto.DepartmentId
            };

            _context.beds.Add(bed);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = bed.Id }, bed);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BedUpdateDto dto)
        {
            var bed = await _context.beds.FindAsync(id);
            if (bed == null) return NotFound();

            bed.BedNumber = dto.BedNumber;
            bed.IsOccupied = dto.IsOccupied;
            bed.DepartmentId = dto.DepartmentId;

            await _context.SaveChangesAsync();
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var bed = await _context.beds.FindAsync(id);
            if (bed == null) return NotFound();

            _context.beds.Remove(bed);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

}
