namespace ArmorFeedApi.Shipments.Domain.Repositories;

public interface IUnitOfWork
{
    Task CompleteAsync();
}