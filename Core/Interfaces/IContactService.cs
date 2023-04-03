using Core.DTO;
using Core.Entities;

namespace Core.Interfaces;

public interface IContactService
{
    Task<List<GetContactDTO>> GetContactsAsync();
    public Task CreateAsync(ContactDTO contactDTO);
}
