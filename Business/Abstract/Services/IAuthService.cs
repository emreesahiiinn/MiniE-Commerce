using Core.Entities.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Security.JWT;
using Entities.DTOs;

namespace Business.Abstract.Services;

public interface IAuthService : IBusinessService
{
    Task<IDataResult<User>> Register(UserForRegisterDto userForRegisterDto, string password);
    Task<IDataResult<User>> Login(UserForLoginDto userForLoginDto);
    Task<IResult> UserExists(string email);
    Task<IDataResult<AccessToken>> CreateAccessToken(User user);
}