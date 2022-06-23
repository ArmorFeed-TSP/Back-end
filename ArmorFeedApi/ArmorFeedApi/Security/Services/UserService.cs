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

    public async Task<IEnumerable<User>> ListAsync()
    {
        return await _userRepository.ListAsync();
    }

    public async Task<User> GetByIdAsync(int id)
    {
        var user = await _userRepository.FindByIdAsync(id);
        if (user == null) throw new KeyNotFoundException("User not found");
        return user;
    }

    public async Task RegisterAsync(RegisterRequest request)
    {
        //Validate
        if (_userRepository.ExitsByEmail(request.Email))
            throw new AppException($"Email '{request.Email}' is already token");
        
        //Map request to user entity
        var user = _mapper.Map<User>(request);
        
        //Hash password
        user.PasswordHash = BCryptNet.HashPassword(request.Password);
        
        //Save User
        try
        {
            await _userRepository.AddAsync(user);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new AppException($"An error occurred while saving the user: {e.Message}");
        }
    }

    public async Task UpdateAsync(int id, UpdateRequest request)
    {
        var user = GetById(id);
        //Validate
        var userWithEmail = await _userRepository.FindByEmailAsync(request.Email);
        if (userWithEmail != null && userWithEmail.Id != user.Id)
            throw new AppException($"Email '{request.Email}' is already taken");
        
        //Hash Password if it was entered
        if (!string.IsNullOrEmpty(request.Password))
            user.PasswordHash = BCryptNet.HashPassword(request.Password);
        
        //Map request to entity
        _mapper.Map(request, user);
        
        //Save User
        try
        {
            _userRepository.Update(user);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new AppException($"An error occurred while updating the user: {e.Message}");
        }

    }

    public async Task DeleteAsync(int id)
    {
        var user = GetById(id);
        try
        {
            _userRepository.Remove(user);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new AppException($"An error occurred while deleting the user: {e.Message}");
        }
    }
    
    //Helper Functions
    private User GetById(int id)
    {
        var user = _userRepository.FindById(id);
        if (user == null) throw new KeyNotFoundException("User not found");
        return user;
    }
}