using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using AutoMapper;
using Core.DTO;
using Core.Entities;
using Core.Interfaces;

namespace Core.Services;

public class ContactService : IContactService
{
    private readonly IRepository<Contact> _repository;
    private readonly IMapper _mapper;

    public ContactService(IRepository<Contact> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<GetContactDTO>> GetContactsAsync()
    {
        var contacts = await _repository.Get(includeProperties: "Accounts");
        return contacts.Select(c => _mapper.Map<GetContactDTO>(c)).ToList();
    }

    public async Task CreateAsync(ContactDTO contactDTO)
    {
        if (string.IsNullOrWhiteSpace(contactDTO.Email) ||
            string.IsNullOrWhiteSpace(contactDTO.FirstName) ||
            string.IsNullOrWhiteSpace(contactDTO.LastName)
           )
            throw new ValidationException("Fields cannot be empty.");
        
        if (await _repository.GetByKeyAsync(contactDTO.Email) != null)
            throw new ValidationException("The contact with such email already exists.");

        var regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        if (!regex.IsMatch(contactDTO.Email))
            throw new ValidationException("Invalid email address.");

        var contact = _mapper.Map<Contact>(contactDTO);
        
        await _repository.AddAsync(contact);
        await _repository.SaveChangesAsync();
    }
}
