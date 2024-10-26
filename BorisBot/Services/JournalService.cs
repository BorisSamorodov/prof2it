using BorisBot.Database.DataObjects;
using BorisBot.Interfaces;
using BorisBot.Models;
using Microsoft.EntityFrameworkCore;

namespace BorisBot.Services;

public class JournalService : IJournalService
{
    private readonly IContextFactory _contextFactory;

    public JournalService(IContextFactory contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<JournalModel[]> GetAll()
    {
        var context = _contextFactory.GetContext();
        var journals = await context.ScientificJournals.Include(x => x.JournalIssues)
            .Select(x => new JournalModel
            {
                Id = x.Id,
                Name = x.Name,
                IssuesCount = x.JournalIssues.Count
            })
            .ToArrayAsync();

        return journals;
    }

    public async Task Update(Guid id, string newName)
    {
        var context = _contextFactory.GetContext();
        var entity = context.ScientificJournals.Single(x => x.Id == id);
        entity.Name = newName;
        await context.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        var context = _contextFactory.GetContext();
        var entity = context.ScientificJournals.Single(x => x.Id == id);
        context.ScientificJournals.Remove(entity);
        await context.SaveChangesAsync();
    }

    public async Task Create(string name)
    {
        var context = _contextFactory.GetContext();
        var entity = new ScientificJournal
        {
            Name = name
        };

        context.ScientificJournals.Add(entity);
        await context.SaveChangesAsync();
    }
}