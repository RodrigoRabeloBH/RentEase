using AutoMapper;
using RentEase.Contratcs;
using RentEase.Domain.Dto;
using RentEase.Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace RentEase.Application.Mapping
{
    [ExcludeFromCodeCoverage]
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PropertyDto, Property>()
                .ForMember(d => d.Images, scr => scr.MapFrom(x => new List<Image>()));

            CreateMap<Property, PropertyCreated>();
            CreateMap<Property, PropertyUpdated>();
        }
    }
}
