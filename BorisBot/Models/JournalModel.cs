namespace BorisBot.Models;

public class JournalModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int IssuesCount { get; set; }
}