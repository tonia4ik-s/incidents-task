using System.ComponentModel.DataAnnotations;
using Infrastructure.Data;
using Infrastructure.DTO;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class IncidentService : IIncidentService
{
    private readonly AppDbContext _dbContext;
    private readonly DbSet<Contact> _contacts;
    private readonly DbSet<Account> _accounts;
    private readonly DbSet<Incident> _incidents;
    private readonly IContactService _contactService;

    public IncidentService(AppDbContext context, IContactService contactService)
    {
        _dbContext = context;
        _contactService = contactService;
        _accounts = context.Set<Account>();
        _incidents = context.Set<Incident>();
        _contacts = context.Set<Contact>();
    }

    public async Task CreateAsync(IncidentCreateDTO incidentDTO)
    {
        var account = await _accounts.FindAsync(incidentDTO.AccountName);
        if (account == null)
            throw new KeyNotFoundException("Account not found.");
        if (account.ContactEmail != null && account.ContactEmail != incidentDTO.Email) 
            throw new ValidationException("Account is linked to another email.");

        var contact = await _contacts.FindAsync(incidentDTO.Email);
        if (contact == null)
        {
            await _contactService
                .CreateAsync(new ContactCreateDTO (
                    incidentDTO.Email,
                    incidentDTO.FirstName,
                    incidentDTO.LastName)
                );
        }
        else
        {
            contact.FirstName = incidentDTO.FirstName;
            contact.LastName = incidentDTO.LastName;
            _contacts.Update(contact);
            await _dbContext.SaveChangesAsync();
        }
        
        account.ContactEmail ??= incidentDTO.Email;

        var incident = new Incident
        {
            Description = incidentDTO.Description,
            DateTime = DateTimeOffset.Now, 
            AccountName = incidentDTO.AccountName
        };

        _accounts.Update(account);
        await _incidents.AddAsync(incident);
        await _dbContext.SaveChangesAsync();
    }
}
