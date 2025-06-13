using Microsoft.EntityFrameworkCore;
using LeaveBackend.Dtos;
using LeaveBackend.Data;
using LeaveBackend.Mapping;
using LeaveBackend.Entities;

namespace LeaveBackend.Endpoints;

public static class LeaveEndpoint {
    const string GetLeaveEndpointName = "GetLeave";

    public static RouteGroupBuilder MapLeaveEndpoint(this WebApplication app) {
        var group = app.MapGroup("leave").WithParameterValidation();

        group.MapGet("/all", async (LeaveContext dbContext) =>
            await dbContext.Leaves
                .Select(leave => leave.ToLeaveDetailsDto())
                .AsNoTracking()
                .ToListAsync()
        );

        group.MapGet("/{id}", async(int id, LeaveContext dbContext) => {
            Leave? leave = await dbContext.Leaves.FindAsync(id);
            return leave is null ? Results.NotFound() : Results.Ok(leave.ToLeaveDetailsDto());
        }).WithName(GetLeaveEndpointName);

        group.MapPost("/create", async(CreateLeaveDto newLeave, LeaveContext dbContext) => {
            Leave leave = newLeave.ToEntity();
            dbContext.Leaves.Add(leave);
            await dbContext.SaveChangesAsync();
            return Results.CreatedAtRoute(GetLeaveEndpointName, new{id = leave.leaveId}, leave.ToLeaveDetailsDto());
        }).WithParameterValidation();

        group.MapPatch("updatestatus/{id}", async (int id, UpdateStatusDto updateStatus, LeaveContext dbContext) =>
        {
            var existingLeave = await dbContext.Leaves.FindAsync(id);
            if (existingLeave is null)
            {
                return Results.NotFound();
            }
            existingLeave.status = updateStatus.status;
            await dbContext.SaveChangesAsync();
            return Results.NoContent();
        });

        group.MapDelete("/delete/{id}", async (int id, LeaveContext dbContext) =>
        {
            await dbContext.Leaves.Where(leave => leave.leaveId == id).ExecuteDeleteAsync();
            return Results.NoContent();
        });

        return group;
    }
}
