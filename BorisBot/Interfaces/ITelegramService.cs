using Telegram.Bot.Types;

namespace BorisBot.Interfaces;

public interface ITelegramService
{
    Task Process(Update update);
}