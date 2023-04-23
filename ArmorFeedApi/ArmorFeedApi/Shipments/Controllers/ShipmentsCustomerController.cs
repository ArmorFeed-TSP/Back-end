using System.Net.Mime;
using ArmorFeedApi.Shipments.Domain.Models;
using ArmorFeedApi.Shipments.Domain.Services;
using ArmorFeedApi.Shipments.Resources;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ArmorFeedApi.Shipments.Controllers;

public class ShipmentsCustomerController
{
    private readonly IShipmentService _shipmentService;
    private readonly IMapper _mapper;

    public ShipmentsCustomerController(IShipmentService shipmentService, IMapper mapper)
    {
        _shipmentService = shipmentService;
        _mapper = mapper;
    }
    
    [HttpGet]
    [Route("api/v1/customers/{customerId}/shipments")]
    public async Task<IEnumerable<ShipmentResource>> GetAllByCustomerId(int customerId)
    {
        var shipments = await _shipmentService.ListByCustomerId(customerId);
        var resources = _mapper.Map<IEnumerable<Shipment>, IEnumerable<ShipmentResource>>(shipments);
        return resources;
    }
}