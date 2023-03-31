using System.ComponentModel.DataAnnotations;
using Infrastructure.DTO;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContactController : Controller
{
    private readonly IContactService _service;

    public ContactController(IContactService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult> CreateAsync([FromQuery]ContactCreateDTO contactDTO)
    {
        try
        {
            await _service.CreateAsync(contactDTO);
        }
        catch (ValidationException e)
        {
            return ValidationProblem(e.Message);
        }
        return Ok("Contact created successfully!");
    }
}
