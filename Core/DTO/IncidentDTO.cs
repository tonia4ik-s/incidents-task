namespace Core.DTO;

public class IncidentDTO
{
    public Guid Name { get; set; }
    public string Description { get; set; }
    public DateTimeOffset DateTime { get; set; }
    public string AccountName { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
