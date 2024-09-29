using BorisBot.Models;
using Microsoft.AspNetCore.Mvc;

namespace BorisBot.Controllers;

[ApiController]
[Route("[controller]")]
public class HomeController : Controller
{
    [HttpGet("index")]
    public IActionResult Index()
    {
        var model = new BaseModel
        {
            Message = "Сторінка в розробці"
        };
        return View(model);
    }
}