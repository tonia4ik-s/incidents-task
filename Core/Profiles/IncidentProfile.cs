using AutoMapper;
using Core.DTO;
using Core.Entities;

namespace Core.Profiles;

public class IncidentProfile : Profile
{
    public IncidentProfile()
    {
        CreateMap<IncidentCreateDTO, Incident>()
            .ForMember(dest => dest.DateTime,
                opt => opt.MapFrom(
                    i => DateTimeOffset.Now));
        CreateMap<Incident, IncidentDTO>()
            .ForMember(dest => dest.Email,
                opt => opt.MapFrom(
                    s => s.Account.ContactEmail))
            .ForMember(dest => dest.FirstName,
                opt => opt.MapFrom(
                    s => s.Account.Contact.FirstName))
            .ForMember(dest => dest.LastName,
                opt => opt.MapFrom(
                    s => s.Account.Contact.LastName));
    }
}
