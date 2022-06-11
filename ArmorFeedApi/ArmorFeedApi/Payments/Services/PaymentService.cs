using ArmorFeedApi.Payments.Domain.Model;
using ArmorFeedApi.Payments.Domain.Repositories;
using ArmorFeedApi.Payments.Domain.Services;
using ArmorFeedApi.Payments.Domain.Services.Communication;
using ArmorFeedApi.Shipments.Domain.Repositories;
using IUnitOfWork = ArmorFeedApi.Shared.Domain.Repositories.IUnitOfWork;

namespace ArmorFeedApi.Payments.Services;

public class PaymentService: IPaymentService
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IShipmentRepository _shipmentRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public PaymentService(IPaymentRepository paymentRepository, IUnitOfWork unitOfWork, IShipmentRepository shipmentRepository)
    {
        _paymentRepository = paymentRepository;
        _shipmentRepository = shipmentRepository;
        _unitOfWork = unitOfWork;
        
    }

    public async Task<IEnumerable<Payment>> ListAsync()
    {
        return await _paymentRepository.ListAsync();
    }

    public async Task<IEnumerable<Payment>> ListByShipmentIdAsync(int shipmentId)
    {
        return await _paymentRepository.FindByShipmentIdAsync(shipmentId);
    }

    public async Task<PaymentResponse> SaveAsync(Payment payment)
    {
        var existingShipment = _shipmentRepository.FindByIdAsync(payment.ShipmentId);
        if (existingShipment == null)
            return new PaymentResponse("Invalid Payment");
        try
        {
            await _paymentRepository.AddAsync(payment);
            await _unitOfWork.CompleteAsync();
            return new PaymentResponse(payment);
        }
        catch (Exception e)
        {
            return new PaymentResponse($"An error occurred while saving the payments: {e.Message}");
        }
    }

    public async Task<PaymentResponse> UpdateAsync(int paymentId, Payment payment)
    {
        var existingPayment = await _paymentRepository.FindByIdAsync(paymentId);
        if (existingPayment == null)
            return new PaymentResponse("Payment not found.");
        
        var existingShipment = await _shipmentRepository.FindByIdAsync(payment.ShipmentId);
        if (existingShipment == null)
            return new PaymentResponse("Invalid Payment");

        existingPayment.Amount = payment.Amount;
        existingPayment.Currency = payment.Currency;
        existingPayment.PaymentDate = payment.PaymentDate;
        existingPayment.ShipmentId = payment.ShipmentId;
        try
        {
            _paymentRepository.Update(existingPayment);
            await _unitOfWork.CompleteAsync();
            return new PaymentResponse(existingPayment);
        }
        catch (Exception e)
        {
            return new PaymentResponse($"An error occurred while updating the tutorial: {e.Message}");
        }
    }

    public async Task<PaymentResponse> DeleteAsync(int paymentId)
    {
        var existingPayment = await _paymentRepository.FindByIdAsync(paymentId);
        if (existingPayment == null)
            return new PaymentResponse("Payment not found.");
        try
        {
            _paymentRepository.Remove(existingPayment);
            await _unitOfWork.CompleteAsync();
            return new PaymentResponse(existingPayment);
        }
        catch (Exception e)
        {
            return new PaymentResponse($"An error occurred while deleting the tutorial: {e.Message}");
        }
    }

}