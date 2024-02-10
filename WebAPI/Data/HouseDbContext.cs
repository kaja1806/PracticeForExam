using Microsoft.EntityFrameworkCore;
using Shared;

namespace WebAPI.Data;

public class HouseDbContext : DbContext
{
 
    public DbSet<House> House { get; set; }
    public DbSet<Owner> Owner { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=House.db");
    }
    

    
}