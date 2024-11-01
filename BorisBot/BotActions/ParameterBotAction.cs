using BorisBot.DTO;

namespace BorisBot.BotActions;

public class ParameterBotAction : BaseBotAction
{
    protected readonly string[] Parameters;

    protected ParameterBotAction(ChatDetails details, string[] parameters) : base(details)
    {
        Parameters = parameters;
    }
}