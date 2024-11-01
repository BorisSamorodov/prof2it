using BorisBot.DTO;
using Microsoft.EntityFrameworkCore;

namespace BorisBot.BotActions.Authors;

public class AuthorGet : ParameterBotAction
{
    public AuthorGet(ChatDetails details, string[] parameters) : base(details, parameters)
    {
    }

    public override async Task Perform()
    {
        var authorId = long.Parse(Parameters.First());
        var author = await Details.Database.Authors.SingleAsync(x => x.Id == authorId);
        
        var markup = new TelegramReplyMarkup();
        markup.AddButtons("Редагувати ім'я", "authors.update/" + authorId, "Призначити редактором", "authors.assign/" + authorId);

        var menu = new TelegramMenu
        {
            ChatId = Details.ChatId,
            Text = author.UserName + " - " + author.RealName,
            Markup = markup
        };

        await ShowButtons(menu);
    }
}