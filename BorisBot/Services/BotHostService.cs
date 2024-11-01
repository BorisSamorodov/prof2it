using BorisBot.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BorisBot.Services;

public class BotHostService : IBotHostService
{
    private readonly bool _isPollMode;
    private readonly ITelegramService _telegramService;
    private readonly TelegramBotClient _client;

    public BotHostService(
        IOptions<BorisBotConfig> config,
        ITelegramService telegramService)
    {
        _client = new TelegramBotClient(config.Value.TelegramToken);
        _telegramService = telegramService;
        _isPollMode = config.Value.IsPollMode;
    }

    public void Start()
    {
        if (!_isPollMode)
        {
            return;
        }
        
        _client.StartReceiving(UpdateHandler, ErrorHandler);
    }

    public bool IsPollMode()
    {
        return _isPollMode;
    }

    private async Task UpdateHandler(ITelegramBotClient client, Update update, CancellationToken token)
    {
        Console.WriteLine(JsonConvert.SerializeObject(update));
        await _telegramService.Process(update);
    }

    private static Task ErrorHandler(ITelegramBotClient client, Exception ex, CancellationToken token )
    {
        Console.WriteLine(ex.ToString());
        return Task.CompletedTask;
    }
}