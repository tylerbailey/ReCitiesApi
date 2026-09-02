using Microsoft.EntityFrameworkCore;
using ReCitiesApi.Infrastructure.Data;
using ReCitiesApi.Models.Dtos;
using ReCitiesApi.Models.Entities;

namespace ReCitiesApi.Server.Services
{
    public class NeighborhoodService(IDbContextFactory<AppDbContext> dbContextFactory) : BaseService(dbContextFactory), INeighborhoodService
    {

        public async Task<List<NeighborhoodDto>> GetAllNeighborhoodsAsync()
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();
            var neighborhoods = await context.Neighborhoods.Where(n => !n.Disabled).ToListAsync();
            var neighborhoodDtos = neighborhoods.Select(n => new NeighborhoodDto
            {
                Id = n.Id,
                Name = n.Name,
                Description = n.Description
                // Map other properties as needed
            }).ToList();
            return neighborhoodDtos;
        }

        public async Task<NeighborhoodDto?> GetNeighborhoodByIdAsync(int id)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();
            var neighborhood = await context.Neighborhoods.FindAsync(id);
            if (neighborhood == null) return null;
            return new NeighborhoodDto
            {
                Id = neighborhood.Id,
                Name = neighborhood.Name,
                Description = neighborhood.Description
                // Map other properties as needed
            };
        }

        public async Task<NeighborhoodDto> CreateNeighborhoodAsync(Neighborhood neighborhood)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();
            context.Neighborhoods.Add(neighborhood);
            await context.SaveChangesAsync();
            return new NeighborhoodDto
            {
                Id = neighborhood.Id,
                Name = neighborhood.Name,
                Description = neighborhood.Description
            };
        }

        public async Task<NeighborhoodDto?> UpdateNeighborhoodAsync(int id, Neighborhood updatedNeighborhood)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();
            var neighborhood = await context.Neighborhoods.FindAsync(id);
            if (neighborhood == null) return null;
            neighborhood.Name = updatedNeighborhood.Name;
            neighborhood.Description = updatedNeighborhood.Description;
            // Update other properties as needed
            await context.SaveChangesAsync();
            return new NeighborhoodDto
            {
                Id = neighborhood.Id,
                Name = neighborhood.Name,
                Description = neighborhood.Description
            };
        }

        public async Task<bool> DeleteNeighborhoodAsync(int id)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();
            var neighborhood = await context.Neighborhoods.FindAsync(id);
            if (neighborhood == null) return false;
            context.Neighborhoods.Remove(neighborhood);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
