using Microsoft.EntityFrameworkCore;
using ReCitiesApi.Infrastructure.Data;

namespace ReCitiesApi.Server.Services
{
    public class BaseService(IDbContextFactory<AppDbContext> dbContextFactory)
    {
        protected readonly IDbContextFactory<AppDbContext> _dbContextFactory = dbContextFactory;
    }
}
