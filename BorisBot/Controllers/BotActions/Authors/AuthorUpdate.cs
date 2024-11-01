using BorisBot.DTO;
using Microsoft.EntityFrameworkCore;

namespace BorisBot.BotActions.Authors;

public class AuthorUpdate : ParameterBotAction
{
    public AuthorUpdate(ChatDetails details, string[] parameters) : base(details, parameters)
    {
    }

    public override async Task Perform()
    {
        var authorId = long.Parse(Parameters.First());
        var author = await Details.Database.Authors.SingleAsync(x => x.Id == authorId);
        await Say("Ім'я для " + author.UserName);

        UserState = new UserStateJson
        {
            IsUpdatingAuthor = true,
            ForAuthorId = authorId
        };
    }
}