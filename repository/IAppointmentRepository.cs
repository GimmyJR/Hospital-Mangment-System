using Hospital_Mangment_System.Dtos;
using Hospital_Mangment_System.Models;

namespace Hospital_Mangment_System.repository
{
    public interface IAppointmentRepository
    {
        Task<Appointment> GetByIdAsync(int id);
        Task<List<AppointmentDto>> GetAllAsync();
        Task<List<AppointmentDto>> GetByPatientIdAsync(string patientId);
        Task<List<AppointmentDto>> GetByDoctorIdAsync(int doctorId);
    }
}
