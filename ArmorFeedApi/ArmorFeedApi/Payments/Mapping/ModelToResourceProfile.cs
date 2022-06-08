using ArmorFeedApi.Payments.Domain.Model;
using ArmorFeedApi.Payments.Resources;
using AutoMapper;

namespace ArmorFeedApi.Payments.Mapping;

public class ModelToResourceProfile: Profile
{
    public ModelToResourceProfile()
    {
        CreateMap<Transaction, TransactionResource>();
    }
}