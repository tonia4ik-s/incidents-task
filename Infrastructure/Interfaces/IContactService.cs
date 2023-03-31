using Infrastructure.DTO;

namespace Infrastructure.Interfaces;

public interface IContactService
{
    public Task CreateAsync(ContactCreateDTO contactDTO);
}
