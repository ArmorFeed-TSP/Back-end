using ArmorFeedApi.Customers.Domain.Models;
using ArmorFeedApi.Customers.Domain.Repositories;
using ArmorFeedApi.Customers.Domain.Services;
using ArmorFeedApi.Customers.Domain.Services.Communication;
using ArmorFeedApi.Shared.Domain.Repositories;

namespace ArmorFeedApi.Customers.Services;

public class CustomerService:ICustomerService
{
     private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public CustomerService(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<IEnumerable<Customer>> ListAsync()
    {
        return await _customerRepository.ListAsync();
    }

    public async Task<CustomerResponse> SaveAsync(Customer customer)
    {
        try
        {
            await _customerRepository.AddAsync(customer);
            await _unitOfWork.CompleteAsync();

            return new CustomerResponse(customer);
        }
        catch (Exception e)
        {
            return new CustomerResponse($"An error occurred while saving the customer: {e.Message}");
        }
    }

    public async Task<CustomerResponse> UpdateAsync(int id, Customer customer)
    {
        var existingCustomer = await _customerRepository.FindByIdAsync(id);

        if (existingCustomer == null)
            return new CustomerResponse("Customer not found.");

        existingCustomer.Name = customer.Name;

        try
        {
            _customerRepository.Update(existingCustomer);
            await _unitOfWork.CompleteAsync();

            return new CustomerResponse(existingCustomer);
        }
        catch (Exception e)
        {
            return new CustomerResponse($"An error occurred while updating the customer: {e.Message}");
        }
    }

    public async Task<CustomerResponse> DeleteAsync(int id)
    {
        var existingCustomer = await _customerRepository.FindByIdAsync(id);

        if (existingCustomer == null)
            return new CustomerResponse("Customer not found.");

        try
        {
            _customerRepository.Remove(existingCustomer);
            await _unitOfWork.CompleteAsync();

            return new CustomerResponse(existingCustomer);
        }
        catch (Exception e)
        {
            return new CustomerResponse($"An error occurred while deleting the customer: {e.Message}");
        }
    }
}