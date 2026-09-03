using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ReCitiesApi.Models.Entities;

namespace ReCitiesApi.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Folder> Folders { get; set; }

        public DbSet<Page> Pages { get; set; }

        public DbSet<Neighborhood> Neighborhoods { get; set; }
    }
}