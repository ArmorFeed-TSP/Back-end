using ArmorFeedApi.Security.Authorization.Handlers.Interfaces;
using ArmorFeedApi.Security.Domain.Models;
using ArmorFeedApi.Security.Domain.Respositories;
using ArmorFeedApi.Security.Domain.Services;
using ArmorFeedApi.Security.Domain.Services.Communication;
using ArmorFeedApi.Security.Exceptions;
using ArmorFeedApi.Shared.Domain.Repositories;
using AutoMapper;
using BCryptNet = BCrypt.Net.BCrypt;

namespace ArmorFeedApi.Security.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    private readonly IJwtHandler _jwtHandler;

    public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, IMapper mapper, IJwtHandler jwtHandler)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _jwtHandler = jwtHandler;
    }

    public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest request)
    {
        var user = await _userRepository.FindByEmailAsync(request.Email);
        Console.WriteLine($"Request: {request.Email}, {request.Password}");
        Console.WriteLine($"User: {user.Id}, {user.Name}, {user.PhoneNumber}, {user.Description}, {user.Ruc}, {user.Email}, {user.PasswordHash}");
        
        //Perform validation
        if (user==null || !BCryptNet.Verify(request.Password,user.PasswordHash))
        {
            Console.WriteLine("Authentication Error");
            throw new AppException("Email or password is incorrect.");
        }
        Console.WriteLine("Authentication succesful. About to generate");
        var response = _mapper.Map<AuthenticateResponse>(user);
        Console.WriteLine($"Response: {response.Id}, {response.Name}, {response.PhoneNumber}, {response.Description}, {response.Ruc}, {response.Email}");
        response.Token = _jwtHandler.GenerateToken(user);
        Console.WriteLine($"Generated token is {response.Token}");
        return response;
    }

    public Task<IEnumerable<User>> ListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<User> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task RegisterAsync(RegisterRequest request)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(int id, UpdateRequest request)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}