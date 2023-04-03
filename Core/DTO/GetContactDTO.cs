using Core.Entities;

namespace Core.DTO;

public class GetContactDTO
{
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public ICollection<AccountDTO> Accounts { get; set; }
}
