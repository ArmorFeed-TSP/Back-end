
using ArmorFeedApi.Shipments.Domain.Models;
using ArmorFeedApi.Shipments.Resources;
using AutoMapper;

namespace ArmorFeedApi.Shared.Mapping;

public class ModelToResourceProfile: Profile
{
    public ModelToResourceProfile()
    {
        CreateMap<Shipment, ShipmentResource>();
        CreateMap<ShipmentReview, ShipmentReviewResource>();
    }
}