using BorisBot.Database.DataObjects;
using BorisBot.DTO;
using Microsoft.EntityFrameworkCore;

namespace BorisBot.BotActions.Issues;

public class IssueGet : ParameterBotAction
{
    public IssueGet(ChatDetails details, string[] parameters) : base(details, parameters)
    {
    }

    public override async Task Perform()
    {
        var issueId = Guid.Parse(Parameters.First());
        var issue = await Details.Database.JournalIssues.SingleAsync(x => x.Id == issueId);
        var buttons = await GetArticleMenuButtons(issue);
        await ShowButtons(buttons);
    }
    
    private async Task<TelegramMenu> GetArticleMenuButtons(JournalIssue issue)
    {
        var markup = new TelegramReplyMarkup();
        var articles = await Details.Database.Articles.Where(x => x.JournalIssue.Id == issue.Id).ToArrayAsync();

        foreach (var a in articles)
        {
            markup.AddButtons(a.Title, "articles.get/" + a.Id);
        }
        
        markup.AddButtons("Додати статтю", "articles.create/" + issue.Id );

        var menu = new TelegramMenu
        {
            ChatId = Details.ChatId,
            Text = "Статті у випуску за " + issue.Date.ToShortDateString(),
            Markup = markup
        };

        return menu;
    }
}