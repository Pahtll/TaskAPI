using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TaskAPI.Abstractions;
using TaskAPI.Application.Services;
using TaskAPI.Domain.Models;

namespace TaskAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<UserResponse>>> GetAllUsers()
    {
        try
        {
            var users = await userService.GetAllUsers();

            var response = users.Select(u =>
                new UserResponse(u.Id, u.Name, u.PasswordHash, u.PhoneNumber, u.CompanyId, u.IsBoss)).ToList();

            return Ok(response);
        }
        catch (ArgumentException e)
        {
            BadRequest(e.ToString());
            throw;
        }
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<UserResponse>> GetUserById(Guid id)
    {
        try
        {
            var user = await userService.GetUserById(id);
            
            var result = new UserResponse(user.Id, user.Name, user.PasswordHash, 
                user.PhoneNumber, user.CompanyId, user.IsBoss);

            return Ok(result);
        }
        catch (ArgumentException e)
        {
            BadRequest(e.ToString());
            throw;
        }
    }

    [HttpGet("{name}")]
    public async Task<ActionResult<UserResponse>> GetUserByName(string name)
    {
        try
        {
            var user = await userService.GetByName(name);

            var result = new UserResponse(user.Id, user.Name, user.PasswordHash, 
                user.PhoneNumber, user.CompanyId, user.IsBoss);
            
            return Ok(result);
        }
        catch (ArgumentException e)
        {
            BadRequest(e.ToString());
            throw;
        }
    }

    [HttpGet("{companyId:guid}")]
    public async Task<ActionResult<List<UserResponse>>> GetAllUsersByCompanyId([FromBody] Guid companyId)
    {
        try
        {
            var users = await userService.GetAllUsersByCompanyId(companyId);
            
            var responses = users.Select(u =>
                new UserResponse(u.Id, u.Name, u.PasswordHash, u.PhoneNumber, u.CompanyId, u.IsBoss)).ToList();
            
            return Ok(responses);
        }
        catch (ArgumentException e)
        {
            BadRequest(e.ToString());
            throw;
        }
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<Guid>> DeleteUser([FromBody] Guid id)
    {
        try
        {
            var deletedId = await userService.DeleteUser(id);
            return Ok(deletedId);
        }
        catch (ArgumentException e)
        {
            BadRequest("User does not exist");
            throw;
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<Guid>> UpdateUser([FromBody] Guid id, string name, string password, string phoneNumber,
        Guid companyId, bool isBoss)
    {
        try
        {
            var updatedId = await userService.UpdateUser(id, name, password, phoneNumber, companyId, isBoss);
            return Ok(updatedId);
        }
        catch (ArgumentException e)
        {
            BadRequest(e.ToString());
            throw;
        }
    }
}