namespace BorisBot.Models;

public class JournalManageModel
{
    public JournalModel[] Existing { get; set; } = Array.Empty<JournalModel>();
    public string NewJournalName { get; set; } = string.Empty;
}