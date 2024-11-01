using BorisBot.DTO;

namespace BorisBot.BotActions.Journals;

public class JournalCreate : BaseBotAction
{
    public JournalCreate(ChatDetails details) : base(details)
    {
    }

    public override async Task Perform()
    {
        UserState = new UserStateJson
        {
            IsCreatingJournal = true
        };
        
        await Say("Введіть назву нового журналу", false);
    }
}