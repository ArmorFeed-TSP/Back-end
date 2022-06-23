using ArmorFeedApi.Security.Domain.Models;
using ArmorFeedApi.Security.Domain.Services.Communication;

namespace ArmorFeedApi.Security.Domain.Services;

public interface IUserService
{
    Task<AuthenticateResponse> Authenticate(AuthenticateRequest request);
    Task<IEnumerable<User>> ListAsync();
    Task<User> GetByIdAsync(int id);
    Task RegisterAsync(RegisterRequest request);
    Task UpdateAsync(int id, UpdateRequest request);
    Task DeleteAsync(int id);
}