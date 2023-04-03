using System.ComponentModel.DataAnnotations;
using Core.DTO;
using Core.Interfaces;
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

    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        var contacts = await _service.GetContactsAsync();
        return Ok(contacts);
    }
    
    [HttpPost]
    public async Task<ActionResult> CreateAsync([FromQuery]ContactDTO contactDTO)
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
