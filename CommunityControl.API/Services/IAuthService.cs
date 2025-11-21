using CommunityControl.API.Models;

namespace CommunityControl.API.Services
{
    public interface IAuthService
    {
        Task<User?> ValidateUserAsync(string email, string password);
        string GenerateToken(User user);
    }
}
