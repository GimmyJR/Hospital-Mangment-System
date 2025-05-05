using AutoMapper;
using Hospital_Mangment_System.Dtos;
using Hospital_Mangment_System.Models;
using Hospital_Mangment_System.repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Mangment_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentsController(AppDbContext context, IWebHostEnvironment env,IAppointmentRepository _appointmentRepository,IMapper mapper)
        {
            _context = context;
            _env = env;
            _mapper = mapper;
            _appointmentRepository = _appointmentRepository;
        }

        [HttpPost("MakeAppointment")]
        public async Task<IActionResult> CreateAppointment([FromForm] AppointmentRequest request)
        {
            // Validate model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Verify doctor exists
            var doctor = await _context.Doctors.FindAsync(request.DoctorId);
            if (doctor == null)
            {
                return BadRequest(new { message = "الطبيب غير موجود" });
            }

            // Create appointment
            var appointment = new Appointment
            {
                PatientName = request.PatientName,
                PatientId = request.PatientId,
                PhoneNumber = request.PhoneNumber,
                DoctorId = request.DoctorId,
                AppointmentDate = request.AppointmentDate,
                AppointmentTime = request.AppointmentTime,
                Status = "Pending"
            };

            // Handle image uploads
            // Handle image uploads
            if (request.Images != null && request.Images.Count > 0)
            {
                // Verify WebRootPath is available
                if (string.IsNullOrEmpty(_env.WebRootPath))
                {
                    return BadRequest("Server configuration error: Uploads directory not available");
                }

                var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");

                try
                {
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    appointment.MedicalImages = new List<MedicalImage>();

                    foreach (var image in request.Images.Where(i => i?.Length > 0))
                    {
                        var uniqueFileName = $"{Guid.NewGuid()}_{image.FileName}";
                        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await image.CopyToAsync(stream);
                        }

                        appointment.MedicalImages.Add(new MedicalImage
                        {
                            FileName = image.FileName,
                            FilePath = $"/uploads/{uniqueFileName}"
                        });
                    }
                }
                catch (Exception ex)
                {
                    // Log the error
                    
                    return StatusCode(500, "Error saving images");
                }
            }

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            // Send confirmation (you'll implement this)
            // await SendConfirmationEmail(appointment);

            return Ok(new
            {
                success = true,
                message = "تم حجز الموعد بنجاح",
                appointmentId = appointment.Id
            });
        }

        [HttpGet("GetAllAppointments")]
        public async Task<ActionResult<IEnumerable<AppointmentDto>>> GetAllAppointments()
        {
            var appointments = _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.MedicalImages);
            return Ok(appointments);
        }

        
        [HttpGet("GetAppointment/{id}")]
        public async Task<ActionResult<AppointmentDto>> GetAppointment(int id)
        {
            var appointment = await _context.Appointments
                .Include(a => a.Doctor)
                .ThenInclude(d => d.appUser)
                .Include(a => a.MedicalImages)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (appointment == null)
            {
                return NotFound(new { message = "Appointment not found" });
            }

            var appointmentDto = new AppointmentDto
            {
                Id = appointment.Id,
                PatientName = appointment.PatientName,
                PatientId = appointment.PatientId,
                PhoneNumber = appointment.PhoneNumber,
                DoctorId = appointment.DoctorId,
                DoctorName = appointment.Doctor?.appUser?.FullName,
                Specialization = appointment.Doctor?.Specialization,
                AppointmentDate = appointment.AppointmentDate,
                FormattedTime = appointment.AppointmentDate.ToString("hh:mm tt"),
                Status = appointment.Status,
                MedicalImageUrls = appointment.MedicalImages?
            .Where(mi => !string.IsNullOrEmpty(mi.FileName))
            .Select(mi => mi.FilePath)
            .ToList() ?? new List<string>()
            };

            return Ok(appointmentDto);
        }

        
        [HttpGet("GetAppointmentsByPatient/{patientId}")]
        public async Task<ActionResult<IEnumerable<AppointmentDto>>> GetAppointmentsByPatient(string patientId)
        {
            var appointments = await _context.Appointments
                .Where(a => a.PatientId == patientId)
                .Include(a => a.Doctor)
                .ThenInclude(d => d.appUser)
                .Include(a => a.MedicalImages)
                .ToListAsync();
            return Ok(appointments);
        }

        
        [HttpGet("GetAppointmentsByDoctor/{doctorId}")]
        public async Task<ActionResult<IEnumerable<AppointmentDto>>> GetAppointmentsByDoctor(int doctorId)
        {
            var appointments =await _context.Appointments
                .Where(a => a.DoctorId == doctorId)
                .Include(a => a.Doctor)
                .ThenInclude(d => d.appUser)
                .Include(a => a.MedicalImages)
                .ToListAsync();
            return Ok(appointments);
        }

    }
}
