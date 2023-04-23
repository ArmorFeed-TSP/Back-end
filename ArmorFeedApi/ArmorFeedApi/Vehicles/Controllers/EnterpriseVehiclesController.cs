
using ArmorFeedApi.Vehicles.Domain.Models;
using ArmorFeedApi.Vehicles.Domain.Services;
using ArmorFeedApi.Vehicles.Resources;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ArmorFeedApi.Vehicles.Controllers;

[ApiController]
[Route("/api/v1/enterprise/{enterpriseId}/vehicles")] 
public class EnterpriseVehiclesController: ControllerBase
{
    private readonly IVehicleService _vehicleService;
    private readonly IMapper _mapper;

    public EnterpriseVehiclesController(IVehicleService vehicleService, IMapper mapper)
    {
        _vehicleService = vehicleService;
        _mapper = mapper;
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get Vehicles",
        Description = "Get All Vehicles by Enterprise Id",
        OperationId = "GetVechicles",
        Tags = new []{"Enterprises"}
    )]
    public async Task<IEnumerable<VehicleResource>> GetAllByEnterpriseId(int enterpriseId)
    {
        var vehicles = await _vehicleService.ListByEnterpriseAsync(enterpriseId);
        var resources = _mapper.Map<IEnumerable<Vehicle>, IEnumerable<VehicleResource>>(vehicles);
        return resources;
    }

    [HttpGet("availables")]
    [SwaggerOperation(
        Summary = "GetVehicles",
        Description = "Get All Available Vehicles by Enterprise Id",
        OperationId = "GetAvailableVechicles",
        Tags = new[] { "Enterprises" }
    )]
    public async Task<IEnumerable<VehicleResource>> GetAllAvailablesByEnterpriseId(int enterpriseId)
    {
        var vehicles = await _vehicleService.ListAllAvailablesByEnterpriseAsync(enterpriseId);
        var resources = _mapper.Map<IEnumerable<Vehicle>, IEnumerable<VehicleResource>>(vehicles);
        return resources;
    }
}