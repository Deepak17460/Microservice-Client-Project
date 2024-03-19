
using AutoMapper;
using Microservice.Client.ProjectAuth.Domain;
using Microservice.Client.ProjectAuth.Models.DTOs;
namespace Microservice.Client.ProjectAuth.Mapper
{
    public class AuthMapper:Profile
    {
        public AuthMapper():base("AuthMapper")
        {
            CreateMap<UserSignUpDTO, User>().ReverseMap()
              .ForMember(c => c.Password, option => option.Ignore())
              .IgnoreAllPropertiesWithAnInaccessibleSetter()
              .IgnoreAllSourcePropertiesWithAnInaccessibleSetter();
            CreateMap<UserDTO, User>().ReverseMap();
            CreateMap<UserSignInDTO, User>().ReverseMap();
           
        }
    }
}
