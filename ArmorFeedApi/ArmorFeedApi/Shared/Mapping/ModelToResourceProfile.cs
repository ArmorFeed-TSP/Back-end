using ArmorFeedApi.Enterprises.Domain.Models;
using ArmorFeedApi.Enterprises.Resources;
using ArmorFeedApi.Vehicles.Domain.Models;
using ArmorFeedApi.Vehicles.Resources;
using AutoMapper;

namespace ArmorFeedApi.Shared.Mapping;

public class ModelToResourceProfile: Profile
{
    public ModelToResourceProfile()
    {
        CreateMap<Enterprise,EnterpriseResource>();
        CreateMap<Vehicle, VehicleResource>();

    }
}