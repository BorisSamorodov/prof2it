using BorisBot.Database;

namespace BorisBot.Interfaces;

public interface IContextFactory
{
    BorisBotContext GetContext();
}