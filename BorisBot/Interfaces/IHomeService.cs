namespace BorisBot.Interfaces;

public interface IHomeService
{
    bool IsHashCorrect(Dictionary<string, string> request, string telegramHash);
    Task AuthorizeUser(HttpContext context, string userName, long id);
    Task AuthorizeLocalAdmin(HttpContext context);
}