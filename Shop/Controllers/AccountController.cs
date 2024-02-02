
using Microsoft.AspNetCore.Mvc;

public class AccountController : Controller
{
    private readonly AccountService _service;

    public AccountController(AccountService service)
    {
        _service = service;
    }

    public IActionResult TestPage()
    {
        return View();
    }

    // GET: AccountController
    public async Task<ActionResult> Index()
    {
        var accounts = await _service.GetAccountAsync();
        return View(accounts);
    }

    // GET: AccountController/Create
    public ActionResult Create()
    {
        return View();
    }

    //    // POST: AccountController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> CreateAsync(AccountDTO Account)
    {
        try
        {
            await _service.InsertAsync(Account);
            await _service.CompletedAsync();

            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }
}


