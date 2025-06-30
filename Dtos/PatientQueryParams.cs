﻿namespace Hospital_Mangment_System.Dtos
{
    // DTOs/PatientQueryParams.cs
    public class PatientQueryParams
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string Search { get; set; }
        public string SortBy { get; set; } = "name";
        public string SortOrder { get; set; } = "asc";
    }
}
