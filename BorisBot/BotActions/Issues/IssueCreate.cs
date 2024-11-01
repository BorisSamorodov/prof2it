using BorisBot.DTO;

namespace BorisBot.BotActions.Issues;

public class IssueCreate : ParameterBotAction
{
    public IssueCreate(ChatDetails details, string[] parameters) : base(details, parameters)
    {
    }

    public override async Task Perform()
    {
        await Say("Введіть дату нового випуску у форматі DD.MM.YYYY", false);
        UserState = new UserStateJson
        {
            IsCreatingIssue = true,
            ForJournalId = Guid.Parse(Parameters.First())
        };
    }
}