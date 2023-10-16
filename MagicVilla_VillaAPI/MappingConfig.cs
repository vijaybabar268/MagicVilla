using AutoMapper;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;

namespace MagicVilla_VillaAPI
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Villa, VillaDto>();
            CreateMap<VillaDto, Villa>();

            CreateMap<VillaDto, VillaCreateDto>().ReverseMap();
            CreateMap<VillaDto, VillaUpdateDto>().ReverseMap();

            CreateMap<Villa, VillaCreateDto>().ReverseMap();
            CreateMap<Villa, VillaUpdateDto>().ReverseMap();
        }
    }
}
