using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace JWTTokenAuth.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UsersController : ControllerBase
{
    [HttpGet]
    public IActionResult GetUsers()
    {
        return Ok(User.FindFirst(ClaimTypes.Name)?.Value);
    }

    [HttpPost]
    [AllowAnonymous]
    public IActionResult LoginUser(string username)
    {
        var keyByte = System.Text.Encoding.UTF8.GetBytes("asda;odbuads;b242342hbiahbasdada");
        var securityKey = new SigningCredentials(new SymmetricSecurityKey(keyByte), SecurityAlgorithms.HmacSha256);

        var security = new JwtSecurityToken(
            issuer:"Project1",
            audience:"Rooom1",
            new Claim[]
            {
                new Claim(ClaimTypes.Name, username)
            },
            expires: DateTime.Now.AddSeconds(10),
            signingCredentials: securityKey);

        var token = new JwtSecurityTokenHandler().WriteToken(security);

        return Ok(token);
    }
}