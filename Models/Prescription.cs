namespace Hospital_Mangment_System.Models
{
    public class Prescription
    {
        public int PrescriptionId { get; set; }
        public string Dosage { get; set; }
        public string MedicalDetails { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
    }
}
