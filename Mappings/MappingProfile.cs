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
        }
    }
}
