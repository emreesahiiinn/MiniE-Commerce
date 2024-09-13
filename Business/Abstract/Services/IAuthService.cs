using Core.Entities.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Security.JWT;
using Entities.DTOs;

namespace Business.Abstract.Services;

public interface IAuthService : IBusinessService
{
    IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password);
    IDataResult<User> Login(UserForLoginDto userForLoginDto);
    IResult UserExists(string email);
    IDataResult<AccessToken> CreateAccessToken(User user);
}