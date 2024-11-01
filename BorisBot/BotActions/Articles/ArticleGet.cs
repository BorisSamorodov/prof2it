using BorisBot.DTO;
using Microsoft.EntityFrameworkCore;

namespace BorisBot.BotActions.Articles;

public class ArticleGet : ParameterBotAction
{
    public ArticleGet(ChatDetails details, string[] parameters) : base(details, parameters)
    {
    }

    public override async Task Perform()
    {
        var articleId = Guid.Parse(Parameters.First());
        var article = await Details.Database.Articles.SingleAsync(x => x.Id == articleId);

        await Say("Будь ласка, стаття на ознайомлення:");
        await SendDocument(article.TelegramFileId, article.Title);
    }
}