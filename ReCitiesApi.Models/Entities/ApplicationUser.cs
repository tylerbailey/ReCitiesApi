using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReCitiesApi.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Column(TypeName = "varchar(256)")]
        public string? DisplayName { get; set; }
        public bool IsApproved { get; set; } = false;


        public int? NeighborhoodId { get; set; }

        public virtual Neighborhood? Neighborhood { get; set; }
    }
}
