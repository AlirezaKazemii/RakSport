using AutoMapper;
using RakSport.Application.DTOs.User;
using RakSport.Domain.Models.User;

namespace RakSport.Application.Mapper.User
{
    public class UserAutoMapper : Profile
    {
        public UserAutoMapper()
        {
            CreateMap<UserEntity,GetCurrentUserDto>().ReverseMap();
        }
    }
}
