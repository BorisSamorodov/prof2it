using BorisBot.Database.DataObjects;
using BorisBot.DTO;
using BorisBot.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace BorisBot.Services;

public class TelegramService : ITelegramService
{
    private readonly BorisBotConfig _config;
    private readonly IContextFactory _contextFactory;
    private readonly IBotActionFactory _botActionFactory;

    public TelegramService(
        IOptions<BorisBotConfig> config,
        IContextFactory contextFactory,
        IBotActionFactory botActionFactory)
    {
        _contextFactory = contextFactory;
        _botActionFactory = botActionFactory;
        _config = config.Value;
    }

    public async Task Process(Update update)
    {
        var details = await GetChatDetails(update);
        if (details == null)
        {
            return;
        }

        var botAction = _botActionFactory.GetBotAction(details);
        while (botAction != null)
        {
            await botAction.Perform();
            botAction = botAction.Next();
        }
    }

    private async Task<ChatDetails?> GetChatDetails(Update update)
    {
        var messageChatId = update.Message?.Chat.Id;
        var callbackChatId = update.CallbackQuery?.Message?.Chat.Id;
        var chatId = messageChatId ?? callbackChatId ?? -1;

        var messageFrom = update.Message?.From?.Id;
        var callbackFrom = update.CallbackQuery?.From.Id;
        var userId = messageFrom ?? callbackFrom ?? -1;

        var fileId = update.Message?.Document?.FileId ?? string.Empty;
        var fileName =  update.Message?.Document?.FileName ?? string.Empty;
        
        var message = update.Message?.Text ?? string.Empty;
        var isCallback = false;
        if (update.Type == UpdateType.CallbackQuery && update.CallbackQuery?.Message != null)
        {
            message = update.CallbackQuery?.Data ?? string.Empty;
            isCallback = true;
        }
        
        var context = _contextFactory.GetContext();
        var author = await context.Authors.FirstOrDefaultAsync(x => x.Id == userId);
        if (author == null)
        {
            author = new Author
            {
                Id = userId,
                UserName = update?.Message?.From?.Username ?? "unparsed"
            };

            context.Authors.Add(author);
            await context.SaveChangesAsync();
        }

        return new ChatDetails(userId, chatId, fileId,  fileName, message, isCallback, context, author, _config);
    }
}