using System.Net.Mime;
using ArmorFeedApi.Shared.Extensions;
using ArmorFeedApi.Vehicles.Domain.Models;
using ArmorFeedApi.Vehicles.Domain.Services;
using ArmorFeedApi.Vehicles.Resources;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ArmorFeedApi.Vehicles.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class VehiclesController: ControllerBase
{
    private readonly IVehicleService _vehicleService;
    private readonly IMapper _mapper;

    public VehiclesController(IVehicleService vehicleService, IMapper mapper)
    {
        _vehicleService = vehicleService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<VehicleResource>> GetAllAsync()
    {
        var vehicles = await _vehicleService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Vehicle>, IEnumerable<VehicleResource>>(vehicles);

        return resources;
    }

    
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveVehicleResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var vehicle = _mapper.Map<SaveVehicleResource, Vehicle>(resource);

        var result = await _vehicleService.SaveAsync(vehicle);

        if (!result.Success)
            return BadRequest(result.Message);

        var vehicleResource = _mapper.Map<Vehicle, VehicleResource>(result.Resource);

        return Ok(vehicleResource);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveVehicleResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var vehicle = _mapper.Map<SaveVehicleResource, Vehicle>(resource);

        var result = await _vehicleService.UpdateAsync(id, vehicle);
        
        if (!result.Success)
            return BadRequest(result.Message);

        var vehicleResource = _mapper.Map<Vehicle, VehicleResource>(result.Resource);

        return Ok(vehicleResource);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _vehicleService.DeleteAsync(id);
        
        if (!result.Success)
            return BadRequest(result.Message);

        var vehicleResource = _mapper.Map<Vehicle, VehicleResource>(result.Resource);

        return Ok(vehicleResource);
        
    }
}