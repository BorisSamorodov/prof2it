using System.ComponentModel.DataAnnotations;

namespace BorisBot.Database.DataObjects;

public class JournalIssue
{
    [Key]
    public Guid Id { get; set; }

    public DateTime Date { get; set; }

    public ScientificJournal ScientificJournal { get; set; } = new();

    public List<Article> Articles { get; set; } = new();
}