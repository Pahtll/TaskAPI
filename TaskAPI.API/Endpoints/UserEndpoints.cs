using TaskAPI.Abstractions;
using TaskAPI.Application.Services;

namespace TaskAPI.Endpoints;

public static class UserEndpoints
{
    public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("register", Register);
        app.MapPost("login", Login);

        return app;
    }

    private static async Task<IResult> Register(RegisterUserRequest userRequest, IUserService userService)
    {
        var newUserId = await userService.Register(userRequest.Name, userRequest.Password, 
            userRequest.PhoneNumber, userRequest.CompanyId, userRequest.IsBoss);

        return Results.Ok(newUserId);
    }

    private static async Task<IResult> Login(LoginUserRequest userRequest, IUserService userService, HttpContext context)
    {
        var token = await userService.Login(userRequest.Name, userRequest.Password);
        
        context.Response.Cookies.Append("Tasty-Cookies", token);

        return Results.Ok(token);
    }
}