using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BLL.Interfaces;
using Entities;
using Factory.WebApi.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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

    [HttpPost("AddUser")]
    public async Task<IActionResult> AddNewUser(User user)
    {
        await _logic.AddNewUser(user);
        return Ok();
    }

    [HttpPost("Login")]
    public async Task<IActionResult> LoginUser(User user)
    {
        var result = await _logic.LoginUser(user);

        return Ok(result);
    }

    [HttpPost("Token")]
    public async Task<IActionResult> LoginByToken(User user)
    {
        var result = await _logic.LoginByTokenUser(user);

        if (result)
        {
            var jwtResult = await AuthorizeJwt(user);    
            return Ok(new {  Token = jwtResult.Item1, Login = jwtResult.Item2});
        }
        
        return Ok(false);
    }

    private async Task<(string, string)> AuthorizeJwt(User user)
    {
        var identity = await GetIdentity(user.Login);
        if (identity == null)
        {
            throw new ArgumentException("user don't exist");
        }

        var now = DateTime.UtcNow;

        var jwt = new JwtSecurityToken(
            issuer: AuthOptions.Issuer,
            audience: AuthOptions.Audience,
            notBefore: now,
            claims: identity.Claims,
            expires: now.Add(AuthOptions.Lifetime),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(),
                SecurityAlgorithms.HmacSha256));
        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
        return (encodedJwt!, identity.Name!);
    }
    
    private async Task<ClaimsIdentity> GetIdentity(string username)
    {
        var user = await _logic.GetUserByLogin(username);
        if (user != null)
        {
            var claims = new List<Claim>
            {
                new (ClaimTypes.NameIdentifier, user.Login),
                new (ClaimTypes.Name, user.Login),
                new ("ChatId", user.ChatId.ToString()),
            };
            
            ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }

        return null;
    }
}