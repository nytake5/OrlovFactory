using BLL.Interfaces;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace Factory.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserLogic _logic;
    
    public UserController(
        IUserLogic logic)
    {
        _logic = logic; 
    }

    [HttpPost]
    public async Task<IActionResult> AddNewUser(User user)
    {
        await _logic.AddNewUser(user);
        return Ok();
    }
}