using ArmorFeedApi.Shared.Domain.Services.Communication;
using ArmorFeedApi.Shipments.Domain.Models;

namespace ArmorFeedApi.Shipments.Domain.Services.Communication;

public class ShipmentReviewResponse: BaseResponse<ShipmentReview>
{
    public ShipmentReviewResponse(ShipmentReview resource) : base(resource)
    {
    }

    public ShipmentReviewResponse(string message) : base(message)
    {
    }
}