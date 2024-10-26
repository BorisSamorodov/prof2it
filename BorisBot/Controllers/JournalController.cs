using BorisBot.Interfaces;
using BorisBot.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BorisBot.Controllers;

[Authorize]
[Route("journals")]
public class JournalController : Controller
{
    private readonly IJournalService _service;

    public JournalController(IJournalService service)
    {
        _service = service;
    }

    [HttpPost("update")]
    public async Task<IActionResult> Update(JournalManageModel model, Guid id)
    {
        await _service.Update(id, model.NewJournalName);
        return RedirectToAction("GetAll");
    }
    
    [HttpPost("create")]
    public async Task<IActionResult> Create(JournalManageModel model)
    {
        await _service.Create(model.NewJournalName);
        return RedirectToAction("GetAll");
    }

    [HttpGet("getAll")]
    public async Task<ViewResult> GetAll()
    {
        var journals = await _service.GetAll();
        var model = new JournalManageModel {Existing = journals};
        return View(model);
    }

    [HttpGet("delete")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.Delete(id);
        return RedirectToAction("GetAll");
    }
}