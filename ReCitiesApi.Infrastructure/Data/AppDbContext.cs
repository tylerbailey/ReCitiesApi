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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Folder>()
                .HasMany(folder => folder.Folders)
                .WithOne()
                .HasForeignKey(folder => folder.ParentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}