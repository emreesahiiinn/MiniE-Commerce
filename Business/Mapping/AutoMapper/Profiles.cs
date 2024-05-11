using AutoMapper;
using Core.Entities.Concrete;
using Entities.DTOs;

namespace Business.Mapping.AutoMapper;

public class Profiles : Profile
{
    public Profiles()
    {
        CreateMap<UserForRegisterDto, User>();
        CreateMap<User, UserForRegisterDto>();
    }
}