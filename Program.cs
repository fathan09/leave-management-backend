using LeaveBackend.Endpoints;
using LeaveBackend.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var port = Environment.GetEnvironmentVariable("PORT") ?? "5114";
builder.WebHost.UseUrls($"http://*:{port}");


var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL") ?? builder.Configuration.GetConnectionString("WebApiDatabase");

builder.Services.AddDbContext<LeaveContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

var app = builder.Build();

app.UseCors("AllowAll");

using(var scope = app.Services.CreateScope()) {
    var dbContext = scope.ServiceProvider.GetRequiredService<LeaveContext>();
    try {
        await dbContext.Database.MigrateAsync();
    } catch(Exception ex) {
        Console.WriteLine($"Datavase Migration Failed: {ex.Message}");

    }
}

app.MapLeaveEndpoint();
app.MapControllers();
app.UseCors("AllowReactApp");

app.Run();
