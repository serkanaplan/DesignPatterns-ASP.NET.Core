using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ObserverDesignPattern.DAL;
using ObserverDesignPattern.Models;
using ObserverDesignPattern.ObserverPattern;

namespace ObserverDesignPattern.Controllers;

public class HomeController(UserManager<AppUser> userManager, ObserverObject observerObject) : Controller
{
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly ObserverObject _observerObject = observerObject;

    [HttpGet]
    public IActionResult Index() => View();


    [HttpPost]
    public async Task<IActionResult> Index(RegisterViewModel model)
    {
        var appUser = new AppUser()
        {
            Name = model.Name,
            Surname = model.Surname,
            Email = model.Email,
            UserName = model.Username
        };
        var result = await _userManager.CreateAsync(appUser, model.Password);
        if (result.Succeeded)
        {
            _observerObject.NotifyObserver(appUser);
            return View();
        }
        
        return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}
