using BorisBot.BotActions;
using BorisBot.BotActions.Articles;
using BorisBot.BotActions.Authors;
using BorisBot.BotActions.Issues;
using BorisBot.BotActions.Journals;
using BorisBot.DTO;
using BorisBot.Interfaces;

namespace BorisBot.Factories;

public class BotActionFactory : IBotActionFactory
{
    public BaseBotAction GetBotAction(ChatDetails details)
    {
        if (!string.IsNullOrEmpty(details.FileId))
        {
            return new ArticleUpload(details);
        }
        
        if (details.IsCallback)
        {
            var split = details.Message.ToLowerInvariant().Split('/');
            var command = split.First();
            var parameters = split.Skip(1).ToArray();
            
            switch (command)
            {
                case "journals.create" : return new JournalCreate(details);
                case "journals.getall" : return new JournalGetAll(details);
                case "journals.get" : return new JournalGet(details, parameters);
                
                case "issues.create" : return new IssueCreate(details, parameters);
                case "issues.get" : return new IssueGet(details, parameters);
                
                case "articles.create" : return new ArticleCreate(details, parameters);
                case "articles.get" : return new ArticleGet(details, parameters);
                
                case "authors.getall" : return new AuthorGetAll(details);
                case "authors.get" : return new AuthorGet(details, parameters);
                case "authors.update" : return new AuthorUpdate(details, parameters);
            }
        }
        else
        {
            switch (details.Message.ToLowerInvariant())
            {
                case "/start" : return new MainMenuBotAction(details);
            }
        }

        return new CustomMessageBotAction(details);
    }
}