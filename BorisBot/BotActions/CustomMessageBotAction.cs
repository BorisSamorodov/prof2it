using BorisBot.BotActions.Authors;
using BorisBot.BotActions.Journals;
using BorisBot.Database.DataObjects;
using BorisBot.DTO;
using BorisBot.Extensions;
using Microsoft.EntityFrameworkCore;

namespace BorisBot.BotActions;

public class CustomMessageBotAction : BaseBotAction
{
    private BaseBotAction? _next;
    
    public CustomMessageBotAction(ChatDetails details) : base(details)
    {
    }

    public override BaseBotAction? Next()
    {
        return _next;
    }

    public override async Task Perform()
    {
        var state = UserState;
        UserState = new UserStateJson();

        if (!state.IsPopulated())
        {
            return;
        }

        if (state.IsCreatingJournal)
        {
            _next = new JournalGetAll(Details);
            await CreateJournal();
        }

        if (state.IsCreatingIssue)
        {
            _next = new JournalGet(Details, new[] {state.ForJournalId.ToString()});
            await CreateIssue(state);
        }

        if (state.IsUpdatingAuthor)
        {
            _next = new AuthorGet(Details, new[] {state.ForAuthorId.ToString()});
            await UpdateAuthor(state.ForAuthorId);
        }
    }

    private async Task UpdateAuthor(long id)
    {
        var author = await Details.Database.Authors.SingleAsync(x => x.Id == id);
        author.RealName = Details.Message;
        await Details.Database.SaveChangesAsync();
    }
    
    private async Task CreateIssue(UserStateJson state)
    {
        var date = Details.Message.AsUtc();
        if (date == null)
        {
            await Say("Помилка в форматі дати");
            return;
        }

        var journal = await Details.Database.ScientificJournals.SingleAsync(x => x.Id == state.ForJournalId);
        var isExists = await Details.Database.JournalIssues.AnyAsync(x => x.Date == date && x.ScientificJournal == journal);
        
        if (isExists)
        {
            await Say("Такий випуск вже є");
            return;
        }

        Details.Database.JournalIssues.Add(new JournalIssue
        {
            ScientificJournal = journal,
            Date = date.Value
        });

        await Details.Database.SaveChangesAsync();
    }
    
    private async Task CreateJournal()
    {
        var name = Details.Message;
        var existing = await Details.Database.ScientificJournals.FirstOrDefaultAsync(x => x.Name == name);
        if (existing != null)
        {
            await Say("Такий журнал вже існує", false);
        }

        Details.Database.ScientificJournals.Add(new ScientificJournal
        {
            Name = name
        });
        
        await Details.Database.SaveChangesAsync();
        await Say( "Журнал створено", false);

        // await ProcessMainMenu(details);
    }
}