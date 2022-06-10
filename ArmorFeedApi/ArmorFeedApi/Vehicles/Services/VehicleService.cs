

using ArmorFeedApi.Shared.Domain.Repositories;
using ArmorFeedApi.Shipments.Domain.Repositories;
using ArmorFeedApi.Vehicles.Domain.Models;
using ArmorFeedApi.Vehicles.Domain.Repositories;
using ArmorFeedApi.Vehicles.Domain.Services;
using ArmorFeedApi.Vehicles.Domain.Services.Communication;

namespace ArmorFeedApi.Vehicles.Services;

public class VehicleService:IVehicleService
{
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEnterpriseRepository _enterpriseRepository;
    

    public VehicleService(IVehicleRepository vehicleRepository, IUnitOfWork unitOfWork,IEnterpriseRepository enterpriseRepository)
    {
        _vehicleRepository = vehicleRepository;
        _unitOfWork = unitOfWork;
        _enterpriseRepository = enterpriseRepository;
    }


    public async Task<IEnumerable<Vehicle>> ListAsync()
    {
        return await _vehicleRepository.ListAsync();
    }

    public async Task<IEnumerable<Vehicle>>ListByEnterpriseIdAsync(int enterpriseId)
    {
        return await _vehicleRepository.FindByEnterpriseIdAsync(enterpriseId);
    }

    public async Task<VehicleResponse> SaveAsync(Vehicle vehicle)
    {
        // Validate Enterprise Id

        var existingEnterprise = _enterpriseRepository.FindByIdAsync(vehicle.EnterpriseId);

        if (existingEnterprise == null)
            return new VehicleResponse("Invalid Enterprise");
        
        //Validate Title

        var existingVehicleWithBrand = await _vehicleRepository.FindByBrandAsync(vehicle.Brand);

        if (existingVehicleWithBrand != null)
            return new VehicleResponse("Vehicle Title already exists");

        try
        {
            await _vehicleRepository.AddAsync(vehicle);
            await _unitOfWork.CompleteAsync();

            return new VehicleResponse(vehicle);
        }
        catch (Exception e)
        {
            return new VehicleResponse($"An error occurred while saving the tutorial: {e.Message}");
        }

    }

    public async Task<VehicleResponse> UpdateAsync(int vehicleId, Vehicle vehicle)
    {
        var existingVehicle = await _vehicleRepository.FindByIdAsync(vehicleId);
        
        // Validate Tutorial Id

        if (existingVehicle == null)
            return new VehicleResponse("Vehicle not found.");

        // Validate Category Id

        var existingEnterprise = _enterpriseRepository.FindByIdAsync(vehicle.EnterpriseId);

        if (existingEnterprise == null)
            return new VehicleResponse("Invalid Enterprise");

// Validate Title

        var existingVehicleWithLicensePlate = await _vehicleRepository.FindByBrandAsync(vehicle.LicensePlate);

        if (existingVehicleWithLicensePlate != null && existingVehicleWithLicensePlate.Id!=existingVehicle.Id)
            return new VehicleResponse("Vehicle LicensePlate already exists");
        
        existingVehicle.LicensePlate= vehicle.LicensePlate;
        existingVehicle.EnterpriseId = vehicle.EnterpriseId;

        try
        {
            _vehicleRepository.Update(existingVehicle);
            await _unitOfWork.CompleteAsync();

            return new VehicleResponse(existingVehicle);
        }
        catch (Exception e)
        {
            return new VehicleResponse($"An error occurred while updating the vehicle: {e.Message}");
        }
    }

    public async Task<VehicleResponse> DeleteAsync(int vehicleId)
    {
        var existingVehicle = await _vehicleRepository.FindByIdAsync(vehicleId);

        if (existingVehicle == null)
            return new VehicleResponse("Tutorial not found.");

        try
        {
            _vehicleRepository.Remove(existingVehicle);
            await _unitOfWork.CompleteAsync();

            return new VehicleResponse(existingVehicle);
        }
        catch (Exception e)
        {
            return new VehicleResponse($"An error occurred while deleting the tutorial: {e.Message}");
        }
    }
}