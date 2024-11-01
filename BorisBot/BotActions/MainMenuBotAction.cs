using BorisBot.DTO;

namespace BorisBot.BotActions;

public class MainMenuBotAction : BaseBotAction
{
    public MainMenuBotAction(ChatDetails details) : base(details)
    {
    }

    public override async Task Perform()
    {
        UserState = new UserStateJson();

        var buttons = GetMainMenuButtons();
        await ShowButtons(buttons);
    }
    
    private TelegramMenu GetMainMenuButtons()
    {
        var markup = new TelegramReplyMarkup();
        markup.AddButtons("Список журналів", "journals.getAll", "Додати журнал", "journals.create");
        markup.AddButtons( "Автори", "authors.getAll", "Редактори", "editors.getAll");

        var menu = new TelegramMenu
        {
            ChatId = Details.ChatId,
            Text = "Головне меню",
            Markup = markup
        };

        return menu;
    }
}