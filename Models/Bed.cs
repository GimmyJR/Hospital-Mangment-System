using System.Text.Json.Serialization;

namespace Hospital_Mangment_System.Models
{
    public class Bed
    {
        public int Id { get; set; }
        public string BedNumber { get; set; }
        public bool IsOccupied { get; set; }

        // Foreign Key
        public int DepartmentId { get; set; }
        [JsonIgnore]
        public Department Department { get; set; }
    }



}
