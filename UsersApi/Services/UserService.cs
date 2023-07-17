using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UsersApi.Data.Dtos;
using UsersApi.Models;

namespace UsersApi.Services;

public class UserService
{
    private IMapper _mapper;
    private UserManager<User> _userManager;
    private SignInManager<User> _signInManager;
    private TokenService _tokenService;

    public UserService(UserManager<User> userManager, IMapper mapper, SignInManager<User> signInManager, TokenService tokenService)
    {
        _userManager = userManager;
        _mapper = mapper;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    public async Task Register(CreateUserDto dto)
    {
        User user = _mapper.Map<User>(dto);

        IdentityResult result = await _userManager.CreateAsync(user, dto.Password);

        if (!result.Succeeded)
        {
            throw new ApplicationException("Could not register user.");
        }
    }

    public async Task<string> Login(LoginUserDto dto)
    {
        var result = await _signInManager.PasswordSignInAsync(dto.Username, dto.Password, false, false);

        if (!result.Succeeded)
        {
            throw new ApplicationException("User authentication failure.");
        }

        var user = _signInManager
            .UserManager
            .Users
            .FirstOrDefault(user => user.NormalizedUserName == dto.Username.ToUpper());

        var token = _tokenService.GenerateToken(user);

        return token;
    }
}
