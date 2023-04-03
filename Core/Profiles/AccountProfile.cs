using AutoMapper;
using Core.DTO;
using Core.Entities;

namespace Core.Profiles;

public class AccountProfile : Profile
{
    public AccountProfile()
    {
        CreateMap<Account, AccountDTO>()
            .ForMember(dest => dest.AccountName,
                opt => opt.MapFrom(
                    s => s.Name))
            .ForMember(dest => dest.Email,
                opt => opt.MapFrom(
                    s => s.ContactEmail)).ReverseMap();
    }
}
