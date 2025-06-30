namespace Hospital_Mangment_System.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        // Navigation
        public ICollection<Doctor> Doctors { get; set; }
        public ICollection<Bed> Beds { get; set; }
    }


}
