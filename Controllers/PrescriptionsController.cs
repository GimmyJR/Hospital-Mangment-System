using Hospital_Mangment_System.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Mangment_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PrescriptionsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddPrescription(Prescription prescription)
        {
            _context.prescriptions.Add(prescription);
            await _context.SaveChangesAsync();
            return Ok(prescription);
        }
    }
}
