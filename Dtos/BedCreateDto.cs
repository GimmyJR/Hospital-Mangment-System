namespace Hospital_Mangment_System.Dtos
{
    public class BedCreateDto
    {
        public string BedNumber { get; set; }
        public bool IsOccupied { get; set; }
        public int DepartmentId { get; set; }
    }


}
