namespace Infrastructure.DTO;

public class ContactCreateDTO
{
    public ContactCreateDTO() { }
    public ContactCreateDTO(string email, string firstName, string lastName)
    {
        Email = email;
        FirstName = firstName;
        LastName = lastName;
    }

    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
