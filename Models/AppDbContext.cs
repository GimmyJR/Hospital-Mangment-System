using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Mangment_System.Models
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext()
        {
            
        }
        public AppDbContext(DbContextOptions options):base(options)
        {
            
        }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<ConsultationForm> ConsultationForms { get; set; }
        public DbSet<Admin> admins { get; set; }
        public DbSet<Prescription> prescriptions { get; set; }
        public DbSet<Question> questions { get; set; }
        public DbSet<MedicationQuestion> MedicationQuestions { get; set; }
        public DbSet<Department> departments { get; set; }
        public DbSet<Bed> beds { get; set; }
        public DbSet<Report> reports { get; set; }

    }
}
