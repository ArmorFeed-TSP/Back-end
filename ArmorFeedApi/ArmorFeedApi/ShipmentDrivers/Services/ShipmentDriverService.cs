using ArmorFeedApi.Customers.Domain.Services.Communication;
using ArmorFeedApi.Security.Authorization.Handlers.Interfaces;
using ArmorFeedApi.Security.Domain.Services.Communication;
using ArmorFeedApi.Security.Exceptions;
using ArmorFeedApi.Shared.Domain.Repositories;
using ArmorFeedApi.Shared.Services;
using ArmorFeedApi.ShipmentDrivers.Domain.Repositories;
using ArmorFeedApi.ShipmentDrivers.Domain.Services;
using ArmorFeedApi.ShipmentDrivers.Domain.Services.Communication;
using AutoMapper;

namespace ArmorFeedApi.ShipmentDrivers.Services;

public class ShipmentDriverService : IShipmentDriverService
{
    private readonly IShipmentDriverRepository _shipmentDriverRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IJwtHandler<ShipmentDriver.Domain.Models.ShipmentDriver> _jwtHandler;
    private readonly SequenceService _sequenceService;

    public ShipmentDriverService(IShipmentDriverRepository shipmentDriverRepository, IUnitOfWork unitOfWork, IMapper mapper, IJwtHandler<ShipmentDriver.Domain.Models.ShipmentDriver> jwtHandler, SequenceService sequenceService)
    {
        _shipmentDriverRepository = shipmentDriverRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _jwtHandler = jwtHandler;
        _sequenceService = sequenceService;
    }

    public async Task<AuthenticateShipmentDriverResponse> Authenticate(AuthenticateRequest request)
    {
        var user = await _shipmentDriverRepository.FindByEmailAsync(request.Email);
        //Console.WriteLine($"Request: {request.Email}, {request.Password}");
        //Console.WriteLine($"User: {user.Id}, {user.Name}, {user.PhoneNumber}, {user.Description}, {user.Ruc}, {user.Email}, {user.PasswordHash}");
        
        //Perform validation
        if (user==null || !BCrypt.Net.BCrypt.Verify(request.Password,user.PasswordHash))
        {
            Console.WriteLine("Authentication Error");
            throw new AppException("Email or password is incorrect.");
        }
        Console.WriteLine("Authentication succesful. About to generate");
        var response = _mapper.Map<AuthenticateShipmentDriverResponse>(user);
        Console.WriteLine($"Response: {response.Id}, {response.Name}, {response.PhoneNumber}, {response.Description}, {response.Ruc}, {response.Email}");
        response.Token = _jwtHandler.GenerateToken(user);
        Console.WriteLine($"Generated token is {response.Token}");
        return response;
    }

    public async Task RegisterAsync(RegisterShipmentDriverRequest request)
    {
        //Validate
        if (_shipmentDriverRepository.ExitsByEmail(request.Email))
            throw new AppException($"Email '{request.Email}' is already token");
        
        //Map request to user entity
        var user = _mapper.Map<ShipmentDriver.Domain.Models.ShipmentDriver>(request);
        user.Id = _sequenceService.IncrementId();
        
        //Hash password
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
        
        //Save User
        try
        {
            await _shipmentDriverRepository.AddAsync(user);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new AppException($"An error occurred while saving the user: {e.Message}");
        }
    }

    private ShipmentDriver.Domain.Models.ShipmentDriver GetById(int id)
    {
        var user = _shipmentDriverRepository.FindById(id);
        if (user == null) throw new KeyNotFoundException("User not found");
        return user;
    }
    
    public async Task UpdateAsync(int id, UpdateShipmentDriverRequest request)
    {
        var user = GetById(id);
        //Validate
        var userWithEmail = await _shipmentDriverRepository.FindByEmailAsync(request.Email);
        if (userWithEmail != null && userWithEmail.Id != user.Id)
            throw new AppException($"Email '{request.Email}' is already taken");
        
        //Hash Password if it was entered
        if (!string.IsNullOrEmpty(request.Password))
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
        
        //Map request to entity
        _mapper.Map(request, user);
        
        //Save User
        try
        {
            _shipmentDriverRepository.Update(user);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new AppException($"An error occurred while updating the user: {e.Message}");
        }
    }

    public async Task<IEnumerable<ShipmentDriver.Domain.Models.ShipmentDriver>> ListAsync()
    {
        return await _shipmentDriverRepository.ListAsync();
    }

    public async Task<ShipmentDriver.Domain.Models.ShipmentDriver> GetByIdAsync(int id)
    {
        return await _shipmentDriverRepository.FindByIdAsync(id);
    }

    public async Task DeleteAsync(int id)
    {
        var user = GetById(id);
        try
        {
            _shipmentDriverRepository.Remove(user);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new AppException($"An error occurred while deleting the user: {e.Message}");
        }
    }
    
    public async Task<IEnumerable<ShipmentDriver.Domain.Models.ShipmentDriver>> GetAllByEnterpriseId(int enterpriseId)
    {
        return await _shipmentDriverRepository.GetAllByEnterpriseId(enterpriseId);
    }
}