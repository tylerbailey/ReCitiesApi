using System;
using System.Collections.Generic;
using System.Text;

namespace ReCitiesApi.Models.Dtos
{
    public class NeighborhoodDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
