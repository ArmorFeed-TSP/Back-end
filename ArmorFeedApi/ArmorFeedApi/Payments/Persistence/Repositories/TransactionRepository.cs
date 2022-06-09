using ArmorFeedApi.Payments.Domain.Model;
using ArmorFeedApi.Payments.Domain.Repositories;
using ArmorFeedApi.Shared.Persistence.Contexts;
using ArmorFeedApi.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ArmorFeedApi.Payments.Persistence.Repositories;

public class TransactionRepository: BaseRepository, ITransactionRepository
{
    public TransactionRepository(AppDbContext context) : base(context){}

    public async Task<IEnumerable<Transaction>> ListAsync()
    {
        return await _context.Transactions
            .Include(p => p.Shipment)
            .ToListAsync();
    }

    public async Task AddAsync(Transaction transaction)
    {
        await _context.Transactions.AddAsync(transaction);
    }

    public async Task<Transaction> FindByIdAsync(int transactionId)
    {
        return await _context.Transactions
            .Include(p => p.Shipment)
            .FirstOrDefaultAsync(p => p.Id == transactionId);
    }

    public async Task<IEnumerable<Transaction>> FindByShipmentIdAsync(int shipmentId)
    {
        return await _context.Transactions
            .Where(p => p.ShipmentId == shipmentId)
            .Include(p => p.Shipment)
            .ToListAsync();
    }

    public void Update(Transaction transaction)
    {
        _context.Transactions.Update(transaction);
    }

    public void Remove(Transaction transaction)
    {
        _context.Transactions.Remove(transaction);
    }
}