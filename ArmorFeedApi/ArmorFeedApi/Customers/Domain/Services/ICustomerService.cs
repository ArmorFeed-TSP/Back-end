
using ArmorFeedApi.Customers.Domain.Models;
using ArmorFeedApi.Customers.Domain.Services.Communication;

namespace ArmorFeedApi.Customers.Domain.Services;

public interface ICustomerService
{
    Task<IEnumerable<Customer>> ListAsync();
    Task<CustomerResponse> SaveAsync(Customer customer);
    Task<CustomerResponse> UpdateAsync(int id, Customer customer);
    Task<CustomerResponse> DeleteAsync(int id);
}