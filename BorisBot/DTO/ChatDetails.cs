using BorisBot.Database;
using BorisBot.Database.DataObjects;

namespace BorisBot.DTO;

public class ChatDetails
{
    public long UserId { get; }
    public long ChatId { get; }
    public string FileId { get; }
    public string FileName { get; }
    
    public string Message { get;  }
    
    public bool IsCallback { get; }
    
    public BorisBotConfig Config { get; }
    
    public BorisBotContext Database { get;  }
    public Author Author { get; }

    public ChatDetails(
        long userId,
        long chatId,
        string fileId,
        string fileName,
        string message,
        bool isCallback,
        BorisBotContext database,
        Author author,
        BorisBotConfig config)
    {
        UserId = userId;
        ChatId = chatId;
        FileId = fileId;
        FileName = fileName;
        Message = message;
        IsCallback = isCallback;
        Database = database;
        Author = author;
        Config = config;
    }
}