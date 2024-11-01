using System.ComponentModel.DataAnnotations;

namespace BorisBot.Database.DataObjects;

public class Article
{
    [Key]
    public Guid Id { get; set; }
    public JournalIssue JournalIssue { get; set; } = new();
    public string Title { get; set; } = string.Empty;

    public string TelegramFileId { get; set; } = string.Empty;
    
    public byte[] Contents { get; set; } = Array.Empty<byte>();
}