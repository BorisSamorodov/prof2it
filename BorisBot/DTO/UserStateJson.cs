namespace BorisBot.DTO;

public class UserStateJson
{
    public bool IsCreatingIssue { get; set; }
    public bool IsCreatingJournal { get; set; }
    public bool IsUploadingArticle { get; set; }
    public bool IsUpdatingAuthor { get; set; }
    public Guid ForJournalId { get; set; }
    public Guid ForIssueId { get; set; }
    public long ForAuthorId { get; set; }

    public bool IsPopulated()
    {
        return IsCreatingIssue || IsCreatingJournal || IsUploadingArticle || IsUpdatingAuthor;
    }
}