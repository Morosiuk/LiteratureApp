using System;

namespace API.DTOs
{
    public class CongregationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Code { get; set; }
        public int Publishers { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
