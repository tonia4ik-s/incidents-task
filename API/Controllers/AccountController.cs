using System.ComponentModel.DataAnnotations;
using Infrastructure.DTO;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
public class AccountController : Controller
{
    private readonly IAccountService _service;

    public AccountController(IAccountService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult> CreateAsync(AccountCreateDTO accountDTO)
    {
        try
        {
            await _service.CreateAsync(accountDTO);
        }
        catch (ValidationException e)
        {
            return ValidationProblem(e.Message);
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
        return Ok("Account created successfully!");
    }
}
