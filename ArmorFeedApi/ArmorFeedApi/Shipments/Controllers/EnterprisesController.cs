﻿using System.Net.Mime;
using ArmorFeedApi.Shared.Extensions;
using ArmorFeedApi.Shipments.Domain.Models;
using ArmorFeedApi.Shipments.Domain.Services;
using ArmorFeedApi.Shipments.Resources;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ArmorFeedApi.Shipments.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class EnterprisesController: ControllerBase
{
    private readonly IEnterpriseService _enterpriseService;
    private readonly IMapper _mapper;

    public EnterprisesController(IEnterpriseService enterpriseService, IMapper mapper)
    {
        _enterpriseService = enterpriseService;
        _mapper = mapper;
    }
    [HttpGet]
    public async Task<IEnumerable<EnterpriseResource>> GetAllSync()
    {
        var enterprises = await _enterpriseService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Enterprise>, IEnumerable<EnterpriseResource>>(enterprises);
        return resources;
    }
    
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveEnterpriseResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var enterprise = _mapper.Map<SaveEnterpriseResource, Enterprise>(resource);

        var result = await _enterpriseService.SaveAsync(enterprise);

        if (!result.Success)
            return BadRequest(result.Message);

        var categoryResource = _mapper.Map<Enterprise, EnterpriseResource>(result.Resource);

        return Ok(categoryResource);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveEnterpriseResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        
        var enterprise = _mapper.Map<SaveEnterpriseResource, Enterprise>(resource);

        var result = await _enterpriseService.UpdateAsync(id, enterprise);
        
        if (!result.Success)
            return BadRequest(result.Message);

        var categoryResource = _mapper.Map<Enterprise, EnterpriseResource>(result.Resource);

        return Ok(categoryResource);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _enterpriseService.DeleteAsync(id);
        
        if (!result.Success)
            return BadRequest(result.Message);

        var categoryResource = _mapper.Map<Enterprise, EnterpriseResource>(result.Resource);

        return Ok(categoryResource);
    }
}