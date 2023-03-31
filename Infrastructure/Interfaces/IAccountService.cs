using Infrastructure.DTO;

namespace Infrastructure.Interfaces;

public interface IAccountService
{
    public Task CreateAsync(AccountCreateDTO accountDTO);
}
