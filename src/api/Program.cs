using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Chefio.Infrastructure.Data;
using Chefio.Application.Interfaces;
using Chefio.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Chefio.Application.Interfaces.Repositories;
using Chefio.Application.Interfaces.Services;
using Chefio.Application.Services;
using Chefio.Api.Examples.Employee;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Mvc;
using Chefio.Api.Middlewares;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;



var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true; 
});

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidateModelStateFilter>();
});

builder.Services.AddControllers()
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
});

// Đăng ký Swagger với options chỉ 1 lần duy nhất
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
    options.ExampleFilters();

    // Thêm cấu hình JWT Bearer
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});


builder.Services.AddSwaggerExamplesFromAssemblyOf<EmployeeCreateRequestExample>();
builder.Services.AddEndpointsApiExplorer();

// DbContext MySQL
builder.Services.AddDbContext<ChefioDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(8, 0, 34))));

// Đăng ký DI
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

var jwtSettings = builder.Configuration.GetSection("Jwt");
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]))
    };
});

var app = builder.Build();  
// Swagger middlewares
app.UseSwagger();
app.UseSwaggerUI();

// Middleware chuẩn
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
