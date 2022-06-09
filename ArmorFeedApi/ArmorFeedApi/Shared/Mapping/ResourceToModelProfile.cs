using ArmorFeedApi.Payments.Domain.Model;
using ArmorFeedApi.Payments.Resources;
using ArmorFeedApi.Shipments.Domain.Models;
using ArmorFeedApi.Shipments.Resources;
using AutoMapper;

namespace ArmorFeedApi.Shared.Mapping;

public class ResourceToModelProfile: Profile
{
    public ResourceToModelProfile()
    {
        CreateMap<SaveTransactionResource, Transaction>();
        CreateMap<SaveShipmentResource, Shipment>();
        CreateMap<SaveShipmentReviewResource, ShipmentReview>();
    }
}