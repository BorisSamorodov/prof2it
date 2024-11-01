using Newtonsoft.Json;

namespace BorisBot.DTO;

public class TelegramButton
{
    [JsonProperty("text")]
    public string Text { get; set; } = string.Empty;
    
    [JsonProperty("callback_data")]
    public string CallbackData { get; set; } = string.Empty;
}