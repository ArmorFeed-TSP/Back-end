using ArmorFeedApi.Payments.Domain.Model;
using ArmorFeedApi.Payments.Domain.Repository;
using ArmorFeedApi.Payments.Domain.Services;
using ArmorFeedApi.Payments.Domain.Services.Communication;
using ArmorFeedApi.Shipments.Domain.Repositories;
using IUnitOfWork = ArmorFeedApi.Shared.Domain.Repositories.IUnitOfWork;

namespace ArmorFeedApi.Payments.Services;

public class TransactionService: ITransactionService
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IShipmentRepository _shipmentRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public TransactionService(ITransactionRepository transactionRepository, IUnitOfWork unitOfWork, IShipmentRepository shipmentRepository)
    {
        _transactionRepository = transactionRepository;
        _shipmentRepository = shipmentRepository;
        _unitOfWork = unitOfWork;
        
    }

    public async Task<IEnumerable<Transaction>> ListAsync()
    {
        return await _transactionRepository.ListAsync();
    }

    public async Task<IEnumerable<Transaction>> ListByShipmentIdAsync(int shipmentId)
    {
        return await _transactionRepository.FindByShipmentIdAsync(shipmentId);
    }

    public async Task<TransactionResponse> SaveAsync(Transaction transaction)
    {
        var existingShipment = _shipmentRepository.FindByIdAsync(transaction.ShipmentId);
        if (existingShipment == null)
            return new TransactionResponse("Invalid Transaction");
        try
        {
            await _transactionRepository.AddAsync(transaction);
            await _unitOfWork.CompleteAsync();
            return new TransactionResponse(transaction);
        }
        catch (Exception e)
        {
            return new TransactionResponse($"An error occurred while saving the transactions: {e.Message}");
        }
    }

    public async Task<TransactionResponse> UpdateAsync(int transactionId, Transaction transaction)
    {
        var existingTransaction = await _transactionRepository.FindByIdAsync(transactionId);
        if (existingTransaction == null)
            return new TransactionResponse("Transaction not found.");
        
        var existingShipment = await _shipmentRepository.FindByIdAsync(transaction.ShipmentId);
        if (existingShipment == null)
            return new TransactionResponse("Invalid Transaction");

        existingTransaction.Amount = transaction.Amount;
        existingTransaction.Currency = transaction.Currency;
        existingTransaction.PaymentDate = transaction.PaymentDate;
        existingTransaction.ShipmentId = transaction.ShipmentId;
        try
        {
            _transactionRepository.Update(existingTransaction);
            await _unitOfWork.CompleteAsync();
            return new TransactionResponse(existingTransaction);
        }
        catch (Exception e)
        {
            return new TransactionResponse($"An error occurred while updating the tutorial: {e.Message}");
        }
    }

    public async Task<TransactionResponse> DeleteAsync(int transactionId)
    {
        var existingTransaction = await _transactionRepository.FindByIdAsync(transactionId);
        if (existingTransaction == null)
            return new TransactionResponse("Transaction not found.");
        try
        {
            _transactionRepository.Remove(existingTransaction);
            await _unitOfWork.CompleteAsync();
            return new TransactionResponse(existingTransaction);
        }
        catch (Exception e)
        {
            return new TransactionResponse($"An error occurred while deleting the tutorial: {e.Message}");
        }
    }

}