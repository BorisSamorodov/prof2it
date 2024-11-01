using Newtonsoft.Json;

namespace BorisBot.DTO;

public class TelegramMenu
{
    [JsonProperty("chat_id")]
    public long ChatId { get; set; }

    [JsonProperty("text")]
    public string Text { get; set; } = string.Empty;
    
    [JsonProperty("reply_markup")]
    public TelegramReplyMarkup Markup { get; set; } = new();
}