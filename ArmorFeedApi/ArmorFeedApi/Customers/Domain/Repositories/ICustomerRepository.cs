using ArmorFeedApi.Customers.Domain.Models;

namespace ArmorFeedApi.Customers.Domain.Repositories;

public interface ICustomerRepository
{
    Task<IEnumerable<Customer>> ListAsync();
    Task AddAsync(Customer customer);
    Task<Customer> FindByIdAsync(int id);
    void Update(Customer customer);
    void Remove(Customer customer);
}