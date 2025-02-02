﻿using System;
using System.Text;
using ECommerce.Application.Services;
using ECommerce.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace ECommerce.Infrastructure;

public static class RegisterService
{
    public static void AddIntrastructureRegister(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ITokenService,TokenService>();

        //Add Auth JWT
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    //ValidateIssuerSigningKey=true,
                    LifetimeValidator = (before,expires,token,param)=>expires>DateTime.UtcNow,
                    ValidIssuer= builder.Configuration["JWT:Issuer"],
                    ValidAudience= builder.Configuration["JWT:Audience"],
                    IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"]))
                };
            });
    }
}

