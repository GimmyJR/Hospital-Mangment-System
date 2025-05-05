using AutoMapper;
using Hospital_Mangment_System.Dtos;
using Hospital_Mangment_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Mangment_System.repository
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public AppointmentRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Appointment> GetByIdAsync(int id)
        {
            return await _context.Appointments
                .Include(a => a.Doctor)
                .ThenInclude(d => d.appUser)
                .Include(a => a.MedicalImages)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<List<AppointmentDto>> GetAllAsync()
        {
            return await _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.MedicalImages)
                .Select(a => new AppointmentDto
                {
                    Id = a.Id,
                    PatientName = a.PatientName,
                    PatientId = a.PatientId,
                    PhoneNumber = a.PhoneNumber,
                    DoctorId = a.DoctorId,
                    DoctorName = "",
                    Specialization = a.Doctor.Specialization,
                    AppointmentDate = a.AppointmentDate,
                    FormattedTime = new DateTime(a.AppointmentTime.Ticks).ToString("h:mm tt"),
                    Status = a.Status,
                    //MedicalImageUrls = a.MedicalImages.Select(i => i.FilePath).ToList()
                })
                .ToListAsync();
        }

        public async Task<List<AppointmentDto>> GetByPatientIdAsync(string patientId)
        {
            return await _context.Appointments
                .Where(a => a.PatientId == patientId)
                .Include(a => a.Doctor)
                .ThenInclude(d => d.appUser)
                .Include(a => a.MedicalImages)
                .Select(a => MapToDto(a))
                .ToListAsync();
        }

        public async Task<List<AppointmentDto>> GetByDoctorIdAsync(int doctorId)
        {
            return await _context.Appointments
                .Where(a => a.DoctorId == doctorId)
                .Include(a => a.Doctor)
                .ThenInclude(d => d.appUser)
                .Include(a => a.MedicalImages)
                .Select(a => MapToDto(a))
                .ToListAsync();
        }

        private static AppointmentDto MapToDto(Appointment a)
        {
            return new AppointmentDto
            {
                Id = a.Id,
                PatientName = a.PatientName,
                PatientId = a.PatientId,
                PhoneNumber = a.PhoneNumber,
                DoctorId = a.DoctorId,
                DoctorName = a.Doctor.appUser.FullName,
                Specialization = a.Doctor.Specialization,
                AppointmentDate = a.AppointmentDate,
                FormattedTime = new DateTime(a.AppointmentTime.Ticks).ToString("h:mm tt"),
                Status = a.Status,
                MedicalImageUrls = a.MedicalImages.Select(i => i.FilePath).ToList()
            };
        }
    }

    
}
