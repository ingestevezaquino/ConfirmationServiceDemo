using AutoMapper;
using CSEAD.API.DTOs;
using CSEAD.Persistence.Models;

namespace CSEAD.API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Facility, FacilityDto>().ReverseMap();
            CreateMap<Diagnostic, DiagnosticDto>().ReverseMap();
        }
    }
}
