using Microsoft.AspNetCore.Mvc;
using UsersApi.Data.Dtos;
using UsersApi.Services;

namespace UsersApi.Controllers;

[ApiController]
[Route("[Controller]")]
public class UserController : ControllerBase
{
    private UserService _userService;

    public UserController(UserService registerService)
    {
        _userService = registerService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser(CreateUserDto dto)
    {
        await _userService.Register(dto);
        return Ok("New user registered.");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserDto dto)
    {
        var token = await _userService.Login(dto);
        return Ok(token);
    }
}
