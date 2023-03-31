using System.ComponentModel.DataAnnotations;
using Infrastructure.Data;
using Infrastructure.DTO;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class AccountService : IAccountService
{
    private readonly DbSet<Account> _accounts;
    private readonly AppDbContext _dbContext;

    public AccountService(AppDbContext dbContext)
    {
        _accounts = dbContext.Set<Account>();
        _dbContext = dbContext;
    }

    public async Task CreateAsync(AccountCreateDTO accountDTO)
    {
        if (string.IsNullOrWhiteSpace(accountDTO.AccountName) ||
            string.IsNullOrWhiteSpace(accountDTO.Email)
            )
            throw new ValidationException("Fields cannot be empty.");
        
        var findAccount = await _accounts.FindAsync(accountDTO.AccountName);
        if (findAccount != null)
        {
            if (findAccount.ContactEmail == accountDTO.Email)
                throw new ValidationException("Account for this email already exists.");
            
            throw new ValidationException("Account name is taken.");
        }

        if (await _dbContext.Contacts.FindAsync(accountDTO.Email) == null)
            throw new KeyNotFoundException("There is no contact related to this email address.");

        var account = new Account
        {
            Name = accountDTO.AccountName,
            ContactEmail = accountDTO.Email
        };
        await _accounts.AddAsync(account);
        await _dbContext.SaveChangesAsync();
    }
}
