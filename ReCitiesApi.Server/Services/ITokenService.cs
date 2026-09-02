using ReCitiesApi.Models.Entities;

namespace ReCitiesApi.Server.Services
{
    public interface ITokenService
    {
        string GenerateToken(ApplicationUser user, IList<string> roles);
    }
}