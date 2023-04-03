using System.ComponentModel.DataAnnotations;
using Core.DTO;
using Core.Interfaces;
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
    
    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        var incidents = await _service.GetAll();
        return Ok(incidents);
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
