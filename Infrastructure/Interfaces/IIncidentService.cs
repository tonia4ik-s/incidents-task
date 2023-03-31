using Infrastructure.DTO;

namespace Infrastructure.Interfaces;

public interface IIncidentService
{
    public Task CreateAsync(IncidentCreateDTO incidentDTO);
}
