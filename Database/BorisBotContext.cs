using Microsoft.EntityFrameworkCore;

namespace BorisBot.Database;

public class BorisBotContext : DbContext
{
    private readonly string _connectionString;

    public BorisBotContext(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionString);
    }
}