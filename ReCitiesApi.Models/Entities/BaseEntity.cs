using System.ComponentModel.DataAnnotations;

namespace ReCitiesApi.Models.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public bool Disabled { get; set; }
    }
}
