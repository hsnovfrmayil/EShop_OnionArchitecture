using System;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ECommerce.Application.Repositories;
using ECommerce.Application.Services;
using ECommerce.Domain.DTOs;
using ECommerce.Domain.Entities.Concretes;
using ECommerce.Domain.Helpers;
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
        //var user = await _readAppUserRepository.GetUserByUserNameAndPassword(loginDTO.UserName,loginDTO.Password);
        var user = await _readAppUserRepository.GetUserByUserName(loginDTO.UserName);
        if (user is null)
            return BadRequest("Invalid username");

        using var hmac = new HMACSHA256(user.PasswordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));

        var isPasswordMatch = computedHash.SequenceEqual(user.PasswordHash);

        if(!isPasswordMatch)
            return BadRequest("Invalid password");

        var accessToken = _tokenService.CreateAccessToken(user);

        var refreshToken = _tokenService.CreateRefreshToken();
        SetRefreshToken(user,refreshToken);

        return Ok(new { token = accessToken });
    }

    [HttpPost("RefreshToken")]
    public async Task<IActionResult> RefreshToken()
    {
        var refreshToken = Request.Cookies["refreshToken"];

        if (string.IsNullOrWhiteSpace(refreshToken))
            return BadRequest("Invalid refresh token");

        var user =await _readAppUserRepository.GetUserByRefreshToken(refreshToken);
        if(user is null)
            return BadRequest("Invalid refresh token");

        var accessToken = _tokenService.CreateAccessToken(user);

        return Ok(new { accessToken=accessToken});
    }


    //Helper Method
    private void SetRefreshToken(AppUser user,RefreshToken refreshToken)
    {
        var cookieOptions = new CookieOptions()
        {
            HttpOnly = true,
            Expires=refreshToken.ExpireTime
        };
        Response.Cookies.Append("refreshToken",refreshToken.Token,cookieOptions);
        user.RefreshToken = refreshToken.Token;
        user.RefreshTokenCreateTime = refreshToken.CreateTime;
        user.RefreshTokenExpireTime = refreshToken.ExpireTime;
    }


    [HttpPost("[action]")]
    public async Task<IActionResult> AddUser([FromBody] AppUserDTO appUserDTO)
    {
        var user = await _readAppUserRepository.GetUserByUserName(appUserDTO.UserName);
        if (user is not null)
            return BadRequest("User already exists");

        using var hmac = new HMACSHA256();
        var newUser = new AppUser()
        {
            UserName = appUserDTO.UserName,
            Email = appUserDTO.Email,
            PasswordHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(appUserDTO.Password)),
            PasswordSalt=hmac.Key,
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

