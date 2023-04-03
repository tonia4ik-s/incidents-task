using Core.DTO;

namespace Core.Interfaces;

public interface IIncidentService
{
    Task<IList<IncidentDTO>> GetAll();
    public Task CreateAsync(IncidentCreateDTO incidentDTO);
}
