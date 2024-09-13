using Business.Abstract.Services;
using Business.Constants;
using Core.Entities.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Entities.DTOs;

namespace Business.Concrete;

public class AuthManager(IUserService userService, ITokenHelper tokenHelper) : IAuthService
{
    public IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password)
    {
        byte[] passwordHash, passwordSalt;
        HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
        var user = new User
        {
            Email = userForRegisterDto.Email,
            Name = userForRegisterDto.Name,
            Surname = userForRegisterDto.Surname,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            Status = true
        };
        userService.Add(user);
        return new SuccessDataResult<User>(user, Messages.UserRegistered);
    }

    public IDataResult<User> Login(UserForLoginDto userForLoginDto)
    {
        var userToCheck = userService.GetByMail(userForLoginDto.Email);
        if (userToCheck == null) return new ErrorDataResult<User>(Messages.UserNotFound);

        if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash,
                userToCheck.PasswordSalt))
            return new ErrorDataResult<User>(Messages.PasswordError);

        return new SuccessDataResult<User>(userToCheck, Messages.SuccessfulLogin);
    }

    public IResult UserExists(string email)
    {
        if (userService.GetByMail(email) != null) return new ErrorResult(Messages.UserAlreadyExists);

        return new SuccessResult(Messages.Success);
    }

    public IDataResult<AccessToken> CreateAccessToken(User user)
    {
        var claims = userService.GetClaims(user);
        var accessToken = tokenHelper.CreateToken(user, claims);
        return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
    }
}