using Business.Abstract.Services;
using Business.Constants;
using Core.Entities.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Business;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Entities.DTOs;

namespace Business.Concrete;

public class AuthManager(IUserService userService, ITokenHelper tokenHelper) : IAuthService
{
    public async Task<IDataResult<AccessToken>> Register(UserForRegisterDto userForRegisterDto)
    {
        var result = BusinessRules.Run(await UserExists(userForRegisterDto.Email));
        if (!result.Status) return new ErrorDataResult<AccessToken>(result.Message);
        byte[] passwordHash, passwordSalt;
        HashingHelper.CreatePasswordHash(userForRegisterDto.Password, out passwordHash, out passwordSalt);
        var user = new User
        {
            Email = userForRegisterDto.Email,
            Name = userForRegisterDto.Name,
            Surname = userForRegisterDto.Surname,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            Status = true
        };
        await userService.Add(user);
        var response = await CreateAccessToken(user);

        return new SuccessDataResult<AccessToken>(response.Data, Messages.UserRegistered);
    }

    public async Task<IDataResult<AccessToken>> Login(UserForLoginDto userForLoginDto)
    {
        var userToCheck = await userService.GetByMail(userForLoginDto.Email);
        if (userToCheck == null) return new ErrorDataResult<AccessToken>(Messages.UserNotFound);

        if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash,
                userToCheck.PasswordSalt))
            return new ErrorDataResult<AccessToken>(Messages.PasswordError);
        var response = await CreateAccessToken(userToCheck);
        return new SuccessDataResult<AccessToken>(response.Data, Messages.SuccessfulLogin);
    }

    public async Task<IResult> UserExists(string email)
    {
        if (await userService.GetByMail(email) != null) return new ErrorResult(Messages.UserAlreadyExists);

        return new SuccessResult(Messages.Success);
    }

    public async Task<IDataResult<AccessToken>> CreateAccessToken(User user)
    {
        var claims = await userService.GetClaims(user);
        var accessToken = tokenHelper.CreateToken(user, claims);
        return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
    }
}