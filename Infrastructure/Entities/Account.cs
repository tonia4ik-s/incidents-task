using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class Account
{
    [Key]
    public string Name { get; set; }
    [ForeignKey("Contact")]
    public string? ContactEmail { get; set;}
    public Contact Contact { get; set; }
    
    public ICollection<Incident> Incidents { get; set; }
}
