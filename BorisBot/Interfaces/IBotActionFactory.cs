using BorisBot.BotActions;
using BorisBot.DTO;

namespace BorisBot.Interfaces;

public interface IBotActionFactory
{
    BaseBotAction GetBotAction(ChatDetails details);
}