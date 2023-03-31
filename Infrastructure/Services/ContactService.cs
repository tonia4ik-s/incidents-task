using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Infrastructure.Data;
using Infrastructure.DTO;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class ContactService : IContactService
{
    private readonly DbSet<Contact> _contacts;
    private readonly AppDbContext _dbContext;

    public ContactService(AppDbContext dbContext)
    {
        _contacts = dbContext.Set<Contact>();
        _dbContext = dbContext;
    }

    public async Task CreateAsync(ContactCreateDTO contactDTO)
    {
        if (string.IsNullOrWhiteSpace(contactDTO.Email) ||
            string.IsNullOrWhiteSpace(contactDTO.FirstName) ||
            string.IsNullOrWhiteSpace(contactDTO.LastName)
           )
            throw new ValidationException("Fields cannot be empty.");
        
        if (await _contacts.FindAsync(contactDTO.Email) != null)
            throw new ValidationException("The contact with such email already exists.");

        var regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        if (!regex.IsMatch(contactDTO.Email))
            throw new ValidationException("Invalid email address.");
        
        var contact = new Contact 
        { 
            Email = contactDTO.Email, 
            FirstName = contactDTO.FirstName,
            LastName = contactDTO.LastName
        };
        await _contacts.AddAsync(contact);
        await _dbContext.SaveChangesAsync();
    }
}
