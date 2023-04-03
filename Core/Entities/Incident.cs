using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

public class Incident
{
    [Key]
    public Guid Name { get; set; }
    public string Description { get; set; }
    public DateTimeOffset DateTime { get; set; }
    [ForeignKey("Account")]
    public string AccountName { get; set; }
    public Account Account { get; set; }
}
