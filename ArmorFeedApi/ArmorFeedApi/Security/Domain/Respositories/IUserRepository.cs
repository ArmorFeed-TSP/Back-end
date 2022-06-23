using ArmorFeedApi.Security.Domain.Models;

namespace ArmorFeedApi.Security.Domain.Respositories;

public interface IUserRepository
{
    Task<IEnumerable<User>> ListAsync();
    Task AddAsync(User user);
    Task<User> FindByIdAsync(int id);
    Task<User> FindByEmailAsync(string email);
    bool ExitsByEmail(string email);
    User FindById(int id);
    void Update(User user);
    void Remove(User user);
}