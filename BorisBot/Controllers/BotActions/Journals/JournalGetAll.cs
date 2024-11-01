using BorisBot.DTO;
using Microsoft.EntityFrameworkCore;

namespace BorisBot.BotActions.Journals;

public class JournalGetAll : BaseBotAction
{
    public JournalGetAll(ChatDetails details) : base(details)
    {
    }

    public override async Task Perform()
    {
        var menu = await GetJournalsMenu();
        await ShowButtons(menu);
    }
    
    private async Task<TelegramMenu> GetJournalsMenu()
    {
        var journals = await Details.Database
            .ScientificJournals
            .Include( x => x.JournalIssues)
            .OrderBy(x => x.Name).ToArrayAsync();
        
        var markup = new TelegramReplyMarkup();
        foreach (var j in journals)
        {
            markup.AddButtons(j.Name + $" ({j.JournalIssues.Count})", "journals.get/" + j.Id);
        }
        
        var menu = new TelegramMenu
        {
            ChatId = Details.ChatId,
            Text = "Список журналів",
            Markup = markup
        };

        return menu;
    }
}