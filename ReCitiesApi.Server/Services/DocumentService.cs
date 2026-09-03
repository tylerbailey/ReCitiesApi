using Microsoft.EntityFrameworkCore;
using ReCitiesApi.Infrastructure.Data;
using ReCitiesApi.Models.Dtos;
using ReCitiesApi.Models.Entities;

namespace ReCitiesApi.Server.Services
{
    public class DocumentService(IDbContextFactory<AppDbContext> dbContextFactory) : BaseService(dbContextFactory), IDocumentService
    {
        public async Task<FolderDto> GetUserStructureAsync(string userId)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();
            var root = await context.Folders.Where(f => f.UserId == userId && f.ParentId == null).FirstOrDefaultAsync() ?? new Folder();
            var folderDto = BuildFolderDtoAsync(root);
            return new FolderDto();
        }

        public async Task CreateFolderAsync(string userId, FolderDto folder)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();
            var newFolder = new Folder
            {
                Name = folder.Name,
                UserId = userId,
                ParentId = folder.ParentId
            };
            context.Folders.Add(newFolder);
            await context.SaveChangesAsync();
        }

        public async Task<FolderDto?> GetFolderByIdAsync(int folderId)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();
            var folder = await context.Folders.Include(f => f.Folders).Include(f => f.Pages).FirstOrDefaultAsync(f => f.Id == folderId);
            if (folder == null) return null;
            return BuildFolderDtoAsync(folder);
        }

        public async Task CreatePageAsync(string userId, PageDto page)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();
            var newPage = new Page
            {
                UserId = userId,
                Title = page.Title,
                Content = page.Content,
                FolderId = page.FolderId
            };
            context.Pages.Add(newPage);
            await context.SaveChangesAsync();
        }

        public async Task<PageDto?> GetPageByIdAsync(int pageId)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();
            var page = await context.Pages.FirstOrDefaultAsync(p => p.Id == pageId);
            if (page == null) return null;
            return new PageDto
            {
                Id = page.Id,
                Title = page.Title,
                Content = page.Content,
                FolderId = page.FolderId
            };
        }


        private FolderDto BuildFolderDtoAsync(Folder root) => new()
        {
            Name = root.Name,
            SubFolders = [.. root.Folders.Select(BuildFolderDtoAsync)],
            Pages = [.. root.Pages.Select(p => new PageDto
            {
                Title = p.Title,
                Content = p.Content
            })]
        };
    }
}
