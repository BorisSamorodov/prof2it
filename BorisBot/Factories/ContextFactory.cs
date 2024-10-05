using BorisBot.Database;
using BorisBot.Interfaces;
using Microsoft.Extensions.Options;

namespace BorisBot.Factories;

public class ContextFactory : IContextFactory
{
    private readonly string _connectionString;

    public ContextFactory(IOptions<BorisBotConfig> config)
    {
        _connectionString = config.Value.ConnectionString;
    }

    public BorisBotContext GetContext()
    {
        return new BorisBotContext(_connectionString);
    }
}