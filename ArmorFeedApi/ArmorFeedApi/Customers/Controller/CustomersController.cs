using System.Net.Mime;
using ArmorFeedApi.Customers.Domain.Models;
using ArmorFeedApi.Customers.Domain.Services;
using ArmorFeedApi.Customers.Resource;
using ArmorFeedApi.Shared.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ArmorFeedApi.Customers.Controller;

[ApiController]
[Route("/api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Create, Read, Update and Delete Customers")]
public class CustomersController: ControllerBase
{
    private readonly ICustomerService _customerService;
    private readonly IMapper _mapper;

    public CustomersController(ICustomerService customerService, IMapper mapper)
    {
        _customerService = customerService;
        _mapper = mapper;
    }
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get All Customers",
        Description = "Get All Customers",
        OperationId = "GetCustomer",
        Tags = new []{"Customers"}
    )]
    public async Task<IEnumerable<CustomerResource>> GetAllSync()
    {
        var customers = await _customerService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerResource>>(customers);
        return resources;
    }
    [HttpPost]
    [SwaggerOperation(
        Summary = "Post Customer",
        Description = "Save Customer In Database",
        OperationId = "PostCustomer",
        Tags = new []{"Customers"}
    )]
    public async Task<IActionResult> PostAsync([FromBody] SaveCustomerResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var customer = _mapper.Map<SaveCustomerResource, Customer>(resource);

        var result = await _customerService.SaveAsync(customer);

        if (!result.Success)
            return BadRequest(result.Message);

        var categoryResource = _mapper.Map<Customer, CustomerResource>(result.Resource);

        return Ok(categoryResource);
    }

}