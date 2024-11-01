using Newtonsoft.Json;

namespace BorisBot.DTO;

public class TelegramReplyMarkup
{
    [JsonProperty("inline_keyboard")]
    public List<TelegramButton[]> Buttons { get; set; } = new();

    public void AddButtons(params string[] textCallbackPairs)
    {
        var buttons = new TelegramButton[textCallbackPairs.Length / 2];

        for (var i = 0; i < textCallbackPairs.Length; i += 2)
        {
            var ix = i / 2;


            buttons[ix] = new TelegramButton
            {
                Text = textCallbackPairs[i],
                CallbackData = textCallbackPairs[i + 1]
            };
        }
        
        Buttons.Add(buttons);
    }
}