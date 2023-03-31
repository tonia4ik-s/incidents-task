using System.ComponentModel.DataAnnotations;
using Infrastructure.DTO;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
public class IncidentController : Controller
{
    private readonly IIncidentService _service;

    public IncidentController(IIncidentService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult> CreateAsync(IncidentCreateDTO incidentDTO)
    {
        try
        {
            await _service.CreateAsync(incidentDTO);
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (ValidationException e)
        {
            return ValidationProblem(e.Message);
        }
        return Ok("Success!");
    }
}
