using AutoMapper;
using Core.DTO;
using Core.Entities;

namespace Core.Profiles;

public class ContactProfile : Profile
{
    public ContactProfile()
    {
        CreateMap<Contact, ContactDTO>().ReverseMap();
        CreateMap<Contact, GetContactDTO>();
    }
}
