using Microsoft.EntityFrameworkCore;
using LeaveBackend.Dtos;
using LeaveBackend.Data;
using LeaveBackend.Entities;

namespace LeaveBackend.Endpoints;

public static class UserEndpoint
{
    const string GetUserEndpointName = "GetUser";
    public static RouteGroupBuilder MapUserEndpoint(this WebApplication app)
    {
        var group = app.MapGroup("user").WithParameterValidation();
        group.MapPost("/login", async (LoginDto loginUser, LeaveContext dbContext) =>
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.username == loginUser.username);
            if (user is null || loginUser.password != user.password)
            {
                return Results.Unauthorized();
            }

            return Results.Ok();
        });

        return group;
    }
}