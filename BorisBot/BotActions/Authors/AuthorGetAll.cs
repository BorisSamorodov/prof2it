using BorisBot.DTO;
using Microsoft.EntityFrameworkCore;

namespace BorisBot.BotActions.Authors;

public class AuthorGetAll : BaseBotAction
{
    public AuthorGetAll(ChatDetails details) : base(details)
    {
    }

    public override async Task Perform()
    {
        var authors = await Details.Database.Authors.OrderBy(x => x.RealName).ToArrayAsync();
        
        var markup = new TelegramReplyMarkup();
        foreach (var a in authors)
        {
            markup.AddButtons($"{a.UserName} - {a.RealName}", "authors.get/" + a.Id);
        }
        
        var menu = new TelegramMenu
        {
            ChatId = Details.ChatId,
            Text = "Список авторів",
            Markup = markup
        };

        await ShowButtons(menu);
    }
}