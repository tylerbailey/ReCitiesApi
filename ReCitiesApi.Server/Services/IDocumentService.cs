using ReCitiesApi.Models.Dtos;

namespace ReCitiesApi.Server.Services
{
    public interface IDocumentService
    {
        Task CreateFolderAsync(string userId, FolderDto folder);
        Task CreatePageAsync(string userId, PageDto page);
        Task<FolderDto?> GetFolderByIdAsync(int folderId);
        Task<PageDto?> GetPageByIdAsync(int pageId);
        Task<FolderDto> GetUserStructureAsync(string userId);
    }
}