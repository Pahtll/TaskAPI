using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using TaskAPI.Application.Services;
using TaskAPI.Domain.Abstractions;
using TaskAPI.Extensions;
using TaskAPI.Infrastructure;
using TaskAPI.Persistence;

var builder = WebApplication.CreateBuilder(args);

var builderConfiguration = builder.Configuration;
var builderServices = builder.Services;

builderServices.AddAuthentication();
builderServices.AddAuthorization();
builderServices.AddEndpointsApiExplorer();
builderServices.AddSwaggerGen();
builderServices.AddApiControllers();

builderServices.Configure<JwtOptions>(builderConfiguration.GetSection(nameof(JwtOptions)));

builderServices.AddDbContext<TaskApiDbContext>(
    options =>
    {
        options.UseNpgsql(builderConfiguration.GetConnectionString(nameof(TaskApiDbContext)));
    });

builderServices.AddScoped<IUserService, UserService>();
builderServices.AddScoped<ICompanyService, CompanyService>();
builderServices.AddScoped<IJwtProvider, JwtProvider>();
builderServices.AddScoped<IPasswordHasher, PasswordHasher>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always
});

app.UseAuthentication();
app.UseAuthorization();

app.AddMappedEndpoints();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();