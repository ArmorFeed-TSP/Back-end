using ArmorFeedApi.Payments.Domain.Model;
using ArmorFeedApi.Payments.Domain.Services.Communication;

namespace ArmorFeedApi.Payments.Domain.Services;

public interface ITransactionService
{
    Task<IEnumerable<Transaction>> ListAsync();
    Task<IEnumerable<Transaction>> ListByShipmentIdAsync(int shipmentId);
    Task<TransactionResponse> SaveAsync(Transaction transaction);
    Task<TransactionResponse> UpdateAsync(int transactionId, Transaction transaction);
    Task<TransactionResponse> DeleteAsync(int transactionId);
}