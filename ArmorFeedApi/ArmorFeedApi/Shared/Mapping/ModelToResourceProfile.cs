using ArmorFeedApi.Payments.Domain.Model;
using ArmorFeedApi.Payments.Resources;
using ArmorFeedApi.Shipments.Domain.Models;
using ArmorFeedApi.Shipments.Resources;
using AutoMapper;

namespace ArmorFeedApi.Shared.Mapping;

public class ModelToResourceProfile: Profile
{
    public ModelToResourceProfile()
    {
        CreateMap<Payment, PaymentResource>();
        CreateMap<Shipment, ShipmentResource>();
        CreateMap<ShipmentReview, ShipmentReviewResource>();
    }
}