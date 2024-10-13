using API.Models.Users;
using API.Services.UserServices.Models;
using AutoMapper;

namespace API.Services.AutoMapper
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<User, UserDetailsResponseModel>();
        }
    }
}
