using ArmorFeedApi.Payments.Domain.Model;
using ArmorFeedApi.Shared.Domain.Services.Communication;

namespace ArmorFeedApi.Payments.Domain.Services.Communication;

public class TransactionResponse : BaseResponse<Transaction>
{
    public TransactionResponse(Transaction resource): base(resource){}
    public TransactionResponse(string message): base(message){}
}