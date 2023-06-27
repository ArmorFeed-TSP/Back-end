using ArmorFeedApi.Customers.Domain.Services.Communication;
using ArmorFeedApi.Customers.Resource;
using ArmorFeedApi.Security.Authorization.Attributes;
using ArmorFeedApi.Security.Domain.Services.Communication;
using ArmorFeedApi.ShipmentDrivers.Domain.Services;
using ArmorFeedApi.ShipmentDrivers.Domain.Services.Communication;
using ArmorFeedApi.ShipmentDrivers.Resources;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ArmorFeedApi.ShipmentDrivers.Controller;

[ApiController]
[Route("api/v1/[controller]")]
public class ShipmentDriverController : ControllerBase
{
    private readonly IShipmentDriverService _shipmentDriverService;
    private readonly IMapper _mapper;

    public ShipmentDriverController(IShipmentDriverService shipmentDriverService, IMapper mapper)
    {
        _shipmentDriverService = shipmentDriverService;
        _mapper = mapper;
    }
    
    [AllowAnonymous]
    [HttpPost("sign-in")]
    public async Task<IActionResult> AuthenticateAsync(AuthenticateRequest request)
    {
        var response = await _shipmentDriverService.Authenticate(request);
        return Ok(response);
    }
    [AllowAnonymous]
    [HttpPost("sign-up")]
    public async Task<IActionResult> RegisterAsync(RegisterShipmentDriverRequest request)
    {
        await _shipmentDriverService.RegisterAsync(request);
        return Ok(new { message ="Registration successful"});
    }
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var users = await _shipmentDriverService.ListAsync();
        var resources = _mapper.Map<IEnumerable<ShipmentDriver.Domain.Models.ShipmentDriver>, IEnumerable<CustomerResource>>(users);
        return Ok(resources);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var user = await _shipmentDriverService.GetByIdAsync(id);
        var resource = _mapper.Map<ShipmentDriver.Domain.Models.ShipmentDriver, ShipmentDriverResource>(user);
        return Ok(resource);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, UpdateShipmentDriverRequest request)
    {
        await _shipmentDriverService.UpdateAsync(id, request);
        return Ok(new { message = "User updated successfully" });
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _shipmentDriverService.DeleteAsync(id);
        return Ok(new { message = "User deleted successfully" });
    }

    [HttpGet("enterprise/{enterpriseId}")]
    public async Task<IActionResult> GetAllByEnterpriseId(int enterpriseId)
    {
        var shipmentDrivers = await _shipmentDriverService.GetAllByEnterpriseId(enterpriseId);
        var resources = _mapper.Map<IEnumerable<ShipmentDriver.Domain.Models.ShipmentDriver>, IEnumerable<ShipmentDriverResource>>(shipmentDrivers);
        return Ok(resources);
    }
}