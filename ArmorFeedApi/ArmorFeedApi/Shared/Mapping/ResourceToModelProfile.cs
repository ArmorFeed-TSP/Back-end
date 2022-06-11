
using ArmorFeedApi.Shipments.Domain.Models;
using ArmorFeedApi.Shipments.Resources;
using ArmorFeedApi.Vehicles.Domain.Models;
using ArmorFeedApi.Vehicles.Resources;
using AutoMapper;

namespace ArmorFeedApi.Shared.Mapping;

public class ResourceToModelProfile: Profile
{
    public ResourceToModelProfile()
    {
        CreateMap<SaveEnterpriseResource, Enterprise>();
        CreateMap<SaveVehicleResource, Vehicle>();
        CreateMap<SaveShipmentResource, Shipment>();
        CreateMap<SaveShipmentReviewResource, ShipmentReview>();
    }
}