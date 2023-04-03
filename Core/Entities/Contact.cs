using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

public class Contact
{
    [Key]
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public ICollection<Account>? Accounts { get; set; }
}
