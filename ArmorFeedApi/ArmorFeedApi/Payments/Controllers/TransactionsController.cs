using System.Net.Mime;
using ArmorFeedApi.Payments.Domain.Model;
using ArmorFeedApi.Payments.Domain.Services;
using ArmorFeedApi.Payments.Resources;
using ArmorFeedApi.Shared.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ArmorFeedApi.Payments.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class TransactionsController : ControllerBase
{
    private readonly ITransactionService _transactionService;
    private readonly IMapper _mapper;

    public TransactionsController(ITransactionService transactionService, IMapper mapper)
    {
        _transactionService = transactionService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<TransactionResource>> GetAllAsync()
    {
        var transactions = await _transactionService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Transaction>, IEnumerable<TransactionResource>>(transactions);

        return resources;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveTransactionResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var transaction = _mapper.Map<SaveTransactionResource, Transaction>(resource);

        var result = await _transactionService.SaveAsync(transaction);

        if (!result.Success)
            return BadRequest(result.Message);

        var transactionResource = _mapper.Map<Transaction, TransactionResource>(result.Resource);

        return Ok(transactionResource);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveTransactionResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var transaction = _mapper.Map<SaveTransactionResource, Transaction>(resource);

        var result = await _transactionService.UpdateAsync(id, transaction);

        if (!result.Success)
            return BadRequest(result.Message);

        var transactionResource = _mapper.Map<Transaction, TransactionResource>(result.Resource);

        return Ok(transactionResource);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _transactionService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var transactionResource = _mapper.Map<Transaction, TransactionResource>(result.Resource);

        return Ok(transactionResource);

    }
}