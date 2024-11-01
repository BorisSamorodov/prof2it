using BorisBot.DTO;
using Microsoft.EntityFrameworkCore;

namespace BorisBot.BotActions.Journals;

public class JournalGet : ParameterBotAction
{
    public JournalGet(ChatDetails details, string[] parameters) : base(details, parameters)
    {
    }

    public override async Task Perform()
    {
        var journalId = Guid.Parse(Parameters.First());
        var menu = await GetSingleJournalMenu(journalId);
        await ShowButtons(menu);
    }
    
    private async Task<TelegramMenu> GetSingleJournalMenu(Guid journalId)
    {
        var issues = await Details.Database.JournalIssues
            .Where(x => x.ScientificJournal.Id == journalId)
            .OrderBy( x => x.Date)
            .ToArrayAsync();
        
        var markup = new TelegramReplyMarkup();
        foreach (var i in issues)
        {
            markup.AddButtons(i.Date.ToShortDateString(), "issues.get/" + i.Id);
        }
        
        markup.AddButtons("Додати випуск", "issues.create/" + journalId);
        
        var menu = new TelegramMenu
        {
            ChatId = Details.ChatId,
            Text = "Випусків журналів",
            Markup = markup
        };

        return menu;
        
    }
}