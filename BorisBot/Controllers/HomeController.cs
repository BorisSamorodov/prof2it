using BorisBot.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;

namespace BorisBot.Controllers;

[Route("home")]
public class HomeController : Controller
{
    private readonly IHomeService _service;
    private readonly ITelegramService _telegramService;

    public HomeController(IHomeService service, ITelegramService telegramService)
    {
        _service = service;
        _telegramService = telegramService;
    }

    [HttpGet("")]
    public async Task<ViewResult> Index()
    {
        var isAuthenticated = User.Identity?.IsAuthenticated == true;
        if (!isAuthenticated && Environment.MachineName.Equals("localhost"))
        {
            await _service.AuthorizeLocalAdmin(HttpContext);
        }
        
        return View();
    }


    [HttpPost("webhook")]
    public async Task<IActionResult> TelegramWebHook(Update update)
    {
        await _telegramService.Process(update);
        return Ok();
    }
    
    [HttpGet("login")]
    public async Task<IActionResult> Login(
        string id,
        // ReSharper disable once InconsistentNaming
        string? first_Name, 
        // ReSharper disable once InconsistentNaming
        string? last_Name,
        string userName,
        // ReSharper disable once InconsistentNaming
        string? auth_Date,
        string hash)
    {
        var meta = new Dictionary<string, string>
        {
            {"id", id},
            {"username", userName}
        };

        if (!string.IsNullOrEmpty(first_Name))
        {
            meta.Add("first_name", first_Name);
        }

        if (!string.IsNullOrEmpty(last_Name))
        {
            meta.Add("last_name", last_Name);
        }
        
        if (!string.IsNullOrEmpty(auth_Date))
        {
            meta.Add("auth_date", auth_Date);
        }

        if (_service.IsHashCorrect(meta, hash))
        {
            await _service.AuthorizeUser(HttpContext, userName, long.Parse(id));
            return RedirectToAction("GetAll", "Journal");
        }

        return RedirectToAction("Index", "Home");
    }

    [HttpGet("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }
}