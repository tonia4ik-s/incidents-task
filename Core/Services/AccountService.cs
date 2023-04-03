using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Core.DTO;
using Core.Entities;
using Core.Interfaces;

namespace Core.Services;

public class AccountService : IAccountService
{
    private readonly IRepository<Account> _accountRepository;
    private readonly IRepository<Contact> _contactRepository;
    private readonly IMapper _mapper;

    public AccountService(
        IRepository<Contact> contactRepository,
        IRepository<Account> accountRepository, IMapper mapper)
    {
        _contactRepository = contactRepository;
        _accountRepository = accountRepository;
        _mapper = mapper;
    }

    public async Task<IList<AccountDTO>> GetAllAsync()
    {
        var accounts = await _accountRepository.Get();
        return accounts.Select(acc => _mapper.Map<AccountDTO>(acc)).ToList();
    }

    public async Task CreateAsync(AccountDTO accountDTO)
    {
        if (string.IsNullOrWhiteSpace(accountDTO.AccountName) ||
            string.IsNullOrWhiteSpace(accountDTO.Email)
            )
            throw new ValidationException("Fields cannot be empty.");
        
        var findAccount = await _accountRepository.GetByKeyAsync(accountDTO.AccountName);
        if (findAccount != null)
        {
            if (findAccount.ContactEmail == accountDTO.Email)
                throw new ValidationException("Account for this email already exists.");
            
            throw new ValidationException("Account name is taken.");
        }

        if (await _contactRepository.GetByKeyAsync(accountDTO.Email) == null)
            throw new KeyNotFoundException("There is no contact related to this email address.");

        var account = _mapper.Map<Account>(accountDTO);
        
        await _accountRepository.AddAsync(account);
        await _accountRepository.SaveChangesAsync();
    }
}
