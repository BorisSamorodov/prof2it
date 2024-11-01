using System.ComponentModel.DataAnnotations;

namespace BorisBot.Database.DataObjects;

public class ScientificJournal
{
    [Key]
    public  Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public List<JournalIssue> JournalIssues { get; set; } = new();
}