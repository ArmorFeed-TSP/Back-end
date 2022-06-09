﻿using ArmorFeedApi.Shipments.Domain.Models;
using ArmorFeedApi.Shipments.Domain.Services.Communication;

namespace ArmorFeedApi.Shipments.Domain.Services;

public interface IShipmentService
{
    Task<IEnumerable<Shipment>> ListAsync();
    Task<ShipmentResponse> SaveAsync(Shipment category);
    Task<ShipmentResponse> UpdateAsync(int id, Shipment category);
    Task<ShipmentResponse> DeleteAsync(int id);
    Task<IEnumerable<Shipment>> ListByEnterpriseId(int enterpriseId);
    Task<IEnumerable<Shipment>> ListByCustomerId(int customerId);
}