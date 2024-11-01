using System.Text;
using BorisBot.DTO;
using Newtonsoft.Json;

namespace BorisBot.BotActions;

public class BaseBotAction
{
    protected readonly ChatDetails Details;

    protected UserStateJson UserState
    {
        get
        {
            var json = Details.Author.UserState;
            if (string.IsNullOrEmpty(json))
            {
                return new UserStateJson();
            }

            return JsonConvert.DeserializeObject<UserStateJson>(json) ?? new UserStateJson();
        }

        set
        {
            Details.Author.UserState = JsonConvert.SerializeObject(value);
            Details.Database.SaveChanges();
        }
    }

    protected BaseBotAction(ChatDetails details)
    {
        Details = details;
    }

    public virtual Task Perform()
    {
        return Task.CompletedTask;
    }

    public virtual BaseBotAction? Next()
    {
        return null;
    }
    
    
    protected async Task Say(string text, bool isHtml = false)
    {
        var token = Details.Config.TelegramToken;
        var parseMode = isHtml ? "&parse_mode=HTML" : string.Empty;
        var url = $"https://api.telegram.org/bot{token}/sendMessage?chat_id={Details.ChatId}{parseMode}&text={text}";

        using var httpClient = new HttpClient();
        await httpClient.GetAsync(url);
    }
    
    protected async Task ShowButtons(TelegramMenu menu)
    {
        var menuJson = JsonConvert.SerializeObject(menu);
        var token = Details.Config.TelegramToken; 
        var content = new StringContent(menuJson, Encoding.UTF8, "application/json");

        using var httpClient = new HttpClient();
        await httpClient.PostAsync($"https://api.telegram.org/bot{token}/sendMessage", content);
    }
    
    protected async Task<byte[]> Download(string fileId)
    {
        var httpClient = new HttpClient
        {
            BaseAddress = new Uri(Details.Config.TelegramDownloadUrl)
        }; 
            
        var request = $"/bot{Details.Config.TelegramToken}/getFile?file_id={fileId}";
        var contents = await httpClient.GetStringAsync(request);
        var parsed = JsonConvert.DeserializeObject<GetFileResponse>(contents);
        request = $"/file/bot{Details.Config.TelegramToken}/{parsed?.Result.FilePath}";
        var bytes = await httpClient.GetByteArrayAsync(request);
        return bytes;
    }
    
    
    protected async Task SendDocument(string fileId, string fileName)
    {
        using var httpClient = new HttpClient();
    
        var url = $"https://api.telegram.org/bot{Details.Config.TelegramToken}/sendDocument";

        var parameters = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("chat_id", Details.ChatId.ToString()),
            new KeyValuePair<string, string>("document", fileId) 
        });

        var response = await httpClient.PostAsync(url, parameters);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            await Say($"Неочікувана помилка: {error}");
        }
    }
}