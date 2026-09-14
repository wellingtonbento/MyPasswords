using AutoMapper;
using MyPasswords.Models;
using MyPasswords.Models.Entities;

namespace MyPasswords.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterViewModel, User>();

            CreateMap<RegisterCredentialViewModel, Credential>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.UserName, opt => opt.Ignore())
                .ForMember(dest => dest.Password, opt => opt.Ignore());

        }
    }
}
