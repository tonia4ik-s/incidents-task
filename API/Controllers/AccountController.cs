using System.ComponentModel.DataAnnotations;
using Core.DTO;
using Core.Interfaces;
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

    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        var accounts = await _service.GetAllAsync();
        return Ok(accounts);
    }
    
    [HttpPost]
    public async Task<ActionResult> CreateAsync(AccountDTO accountDTO)
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
