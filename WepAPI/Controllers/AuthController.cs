using Business.Abstract.Services;
using Core.Entities.Abstract;
using Core.Utilities.Security.JWT;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WepAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthService authService) : Controller
{
    [HttpPost("Login")]
    public async  Task<IDataResult<AccessToken>> Login(UserForLoginDto userForLoginDto)
    {
        var result = await authService.Login(userForLoginDto);
        HttpContext.Items["Result"] = result;
        return result;
    }

    [HttpPost("Register")]
    public async Task<IDataResult<AccessToken>> Register(UserForRegisterDto userForRegisterDto)
    {
        var result = await authService.Register(userForRegisterDto);
        HttpContext.Items["Result"] = result;
        return result;
    }
}