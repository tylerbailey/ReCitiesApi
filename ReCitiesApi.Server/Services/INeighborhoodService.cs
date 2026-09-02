using ReCitiesApi.Models.Dtos;
using ReCitiesApi.Models.Entities;

namespace ReCitiesApi.Server.Services
{
    public interface INeighborhoodService
    {
        Task<NeighborhoodDto> CreateNeighborhoodAsync(Neighborhood neighborhood);
        Task<bool> DeleteNeighborhoodAsync(int id);
        Task<List<NeighborhoodDto>> GetAllNeighborhoodsAsync();
        Task<NeighborhoodDto?> GetNeighborhoodByIdAsync(int id);
        Task<NeighborhoodDto?> UpdateNeighborhoodAsync(int id, Neighborhood updatedNeighborhood);
    }
}