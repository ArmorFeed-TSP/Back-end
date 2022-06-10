
using ArmorFeedApi.Shipments.Domain.Models;
using ArmorFeedApi.Shipments.Resources;
using AutoMapper;

namespace ArmorFeedApi.Shared.Mapping;

public class ResourceToModelProfile: Profile
{
    public ResourceToModelProfile()
    {
        CreateMap<SaveShipmentResource, Shipment>();
        CreateMap<SaveShipmentReviewResource, ShipmentReview>();
    }
}