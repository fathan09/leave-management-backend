using Microsoft.EntityFrameworkCore;
using LeaveBackend.Entities;

namespace LeaveBackend.Data;

public class LeaveContext : DbContext {

    protected readonly IConfiguration Configuration;

    public LeaveContext(IConfiguration configuration) {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options) {
        options.UseNpgsql(Configuration.GetConnectionString("WebApiDatabase"));
    }

    public DbSet<Leave> Leaves {get; set;}

    public DbSet<User> Users { get; set; }
}
