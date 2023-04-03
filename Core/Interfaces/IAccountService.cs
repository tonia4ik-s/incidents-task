using Core.DTO;

namespace Core.Interfaces;

public interface IAccountService
{
    Task<IList<AccountDTO>> GetAllAsync();
    public Task CreateAsync(AccountDTO accountDTO);
}
