using BorisBot.Database.DataObjects;
using Microsoft.EntityFrameworkCore;

namespace BorisBot.Database;

public class BorisBotContext : DbContext
{
    private readonly string _connectionString;

    public DbSet<Article> Articles { get; protected set; } = null!;
    public DbSet<ArticleAuthor> ArticleAuthors { get; protected set; } = null!;
    public DbSet<Author> Authors { get; protected set; } = null!;
    public DbSet<Editor> Editors { get; protected set; } = null!;
    public DbSet<JournalIssue> JournalIssues { get; protected set; } = null!;
    public DbSet<ScientificJournal> ScientificJournals { get; protected set; } = null!;

    public BorisBotContext(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_connectionString);
    }
}