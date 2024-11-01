using BorisBot.DTO;
using Microsoft.EntityFrameworkCore;

namespace BorisBot.BotActions.Articles;

public class ArticleCreate : ParameterBotAction
{
    public ArticleCreate(ChatDetails details, string[] parameters) : base(details, parameters)
    {
    }

    public override async Task Perform()
    {
        var issueId = Guid.Parse(Parameters.First());
        var issue = await Details.Database.JournalIssues
            .Include(x => x.ScientificJournal)
            .SingleAsync(x => x.Id == issueId);

        await Say($"Завантажте в чат статтю для журналу {issue.ScientificJournal.Name}, випуск за {issue.Date.ToShortDateString()}");

        UserState = new UserStateJson
        {
            IsUploadingArticle = true,
            ForIssueId = issueId
        };
    }
}