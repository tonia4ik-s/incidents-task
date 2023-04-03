using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Core.DTO;
using Core.Entities;
using Core.Interfaces;

namespace Core.Services;

public class IncidentService : IIncidentService
{
    private readonly IRepository<Contact> _contactRepository;
    private readonly IRepository<Account> _accountRepository;
    private readonly IRepository<Incident> _incidentRepository;
    private readonly IContactService _contactService;
    private readonly IMapper _mapper;

    public IncidentService(
        IContactService contactService,
        IRepository<Contact> contactRepository,
        IRepository<Account> accountRepository,
        IRepository<Incident> incidentRepository, IMapper mapper)
    {
        _contactService = contactService;
        _contactRepository = contactRepository;
        _accountRepository = accountRepository;
        _incidentRepository = incidentRepository;
        _mapper = mapper;
    }
    
    public async Task<IList<IncidentDTO>> GetAll()
    {
        var incidents = await _incidentRepository.Get(includeProperties: 
            $"{nameof(Account)},Account.Contact" );
        return incidents.Select(incident => _mapper.Map<IncidentDTO>(incident)).ToList();
    }

    public async Task CreateAsync(IncidentCreateDTO incidentDTO)
    {
        var account = await _accountRepository.GetByKeyAsync(incidentDTO.AccountName);
        if (account == null)
            throw new KeyNotFoundException("Account not found.");
        if (account.ContactEmail != null && account.ContactEmail != incidentDTO.Email) 
            throw new ValidationException("Account is linked to another email.");

        var contact = await _contactRepository.GetByKeyAsync(incidentDTO.Email);
        if (contact == null)
        {
            await _contactService
                .CreateAsync(new ContactDTO (
                    incidentDTO.Email,
                    incidentDTO.FirstName,
                    incidentDTO.LastName)
                );
        }
        else
        {
            contact.FirstName = incidentDTO.FirstName;
            contact.LastName = incidentDTO.LastName;
            await _contactRepository.UpdateAsync(contact);
            await _contactRepository.SaveChangesAsync();
        }
        
        account.ContactEmail ??= incidentDTO.Email;

        var incident = _mapper.Map<Incident>(incidentDTO);

        await _accountRepository.UpdateAsync(account);
        await _incidentRepository.AddAsync(incident);
        await _incidentRepository.SaveChangesAsync();
    }
}
