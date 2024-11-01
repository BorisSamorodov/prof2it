using BorisBot.Database.DataObjects;
using BorisBot.DTO;
using Microsoft.EntityFrameworkCore;

namespace BorisBot.BotActions.Articles;

public class ArticleUpload : BaseBotAction
{
    public ArticleUpload(ChatDetails details) : base(details)
    {
    }

    public override async Task Perform()
    {
        var state = UserState;
        UserState = new UserStateJson();

        if (!state.IsUploadingArticle || state.ForIssueId == Guid.Empty)
        {
            await Say("Вибачте, файл очікується лише після 'Додати статтю'");
            return;
        }

        var journalIssue = await Details.Database.JournalIssues.SingleAsync(x => x.Id == state.ForIssueId);
        var bytes = await Download(Details.FileId);

        var isExists = await Details.Database.Articles.AnyAsync(x => x.Title == Details.FileName);
        if (isExists)
        {
            await Say("Вибачте, стаття з такоє назвою вже існує");
            return;
        }

        var article = new Article
        {
            Contents = bytes,
            JournalIssue = journalIssue,
            TelegramFileId = Details.FileId,
            Title = Details.FileName
        };
        
        Details.Database.Articles.Add(article);

        Details.Database.ArticleAuthors.Add(new ArticleAuthor
        {
            Article = article,
            Author = Details.Author
        });

        await Details.Database.SaveChangesAsync();
        await Say("Статтю прийнято");
    }
}