using ArmorFeedApi.Payments.Domain.Model;
using ArmorFeedApi.Payments.Resources;
using AutoMapper;

namespace ArmorFeedApi.Payments.Mapping;

public class ResourceToModelProfile: Profile
{
    public ResourceToModelProfile()
    {
        CreateMap<SaveTransactionResource, Transaction>();
    }
}