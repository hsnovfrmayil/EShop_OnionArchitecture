using System;
using System.Security.Claims;
using ECommerce.Application.Repositories;
using ECommerce.Application.Services;
using ECommerce.Domain.DTOs;
using ECommerce.Domain.Entities.Concretes;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebApi.Controllerr;

[Route("api/[controller]")]
public class AuthController:ControllerBase
{
    private readonly IReadAppUserRepository _readAppUserRepository;
    private readonly IWriteAppUserRepository _writeAppUserRepository;
    private readonly ITokenService _tokenService;

    public AuthController(IReadAppUserRepository readAppUserRepository, ITokenService tokenService, IWriteAppUserRepository writeAppUserRepository)
    {
        _readAppUserRepository = readAppUserRepository;
        _tokenService = tokenService;
        _writeAppUserRepository = writeAppUserRepository;
    }


    [HttpPost("[action]")]
    public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
    {
        var user = await _readAppUserRepository.GetUserByUserNameAndPassword(loginDTO.UserName,loginDTO.Password);
        if (user is null)
            return BadRequest("Invalid username or password");

        var token = _tokenService.CreateToken(user);
        return Ok(new { token = token });
    }


    [HttpPost("[action]")]
    public async Task<IActionResult> AddUser([FromBody] AppUserDTO appUserDTO)
    {
        var user = await _readAppUserRepository.GetUserByUserName(appUserDTO.UserName);
        if (user is not null)
            return BadRequest("User already exists");

        var newUser = new AppUser()
        {
            UserName = appUserDTO.UserName,
            Email = appUserDTO.Email,
            Password = appUserDTO.Password,
            Role = appUserDTO.Role
        };

        await _writeAppUserRepository.AddAsync(newUser);
        await _writeAppUserRepository.SaveChangesAsync();
        return Ok();
    }

    [HttpPost("[action]")]
    public IActionResult SomeMethod()
    {
        var identity=HttpContext.User.Identity as ClaimsIdentity;
        var claims = identity.Claims;

        var user = new AppUser()
        {
            UserName = claims.FirstOrDefault(p => p.Type == ClaimTypes.Name)?.Value,
            Email = claims.FirstOrDefault(p => p.Type == ClaimTypes.Email)?.Value,
            Role = claims.FirstOrDefault(p => p.Type == ClaimTypes.Role)?.Value,
        };

        return Ok(user);
    }
}

