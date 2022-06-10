
using ArmorFeedApi.Shared.Domain.Services.Communication;
using ArmorFeedApi.Shipments.Domain.Models;

namespace ArmorFeedApi.Shipments.Domain.Services.Communication;

public class EnterpriseResponse: BaseResponse<Enterprise>
{
    public EnterpriseResponse(Enterprise resource) : base(resource)
    {
    }

    public EnterpriseResponse(string message) : base(message)
    {
    }
}