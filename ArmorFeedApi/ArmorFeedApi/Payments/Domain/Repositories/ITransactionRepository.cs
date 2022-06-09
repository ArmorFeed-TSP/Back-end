using ArmorFeedApi.Payments.Domain.Model;

namespace ArmorFeedApi.Payments.Domain.Repositories;

public interface ITransactionRepository
{
    Task<IEnumerable<Transaction>> ListAsync();
    Task AddAsync(Transaction transaction);
    Task<Transaction> FindByIdAsync(int transactionId);
    Task<IEnumerable<Transaction>> FindByShipmentIdAsync(int shipmentId);
    void Update(Transaction transaction);
    void Remove(Transaction transaction);

}