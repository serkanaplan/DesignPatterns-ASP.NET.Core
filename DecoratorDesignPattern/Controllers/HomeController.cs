using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DecoratorDesignPattern.Models;
using DecoratorDesignPattern.DAL;
using DecoratorDesignPattern.DecoratorPattern2;

namespace DecoratorDesignPattern.Controllers;

public class HomeController : Controller
{
    [HttpGet]
    public IActionResult Index() => View();


    [HttpPost]
    public IActionResult Index(Message message)
    {
        CreateNewMessage createNewMessage = new();
        createNewMessage.SendMessage(message);
        return View();
    }


    [HttpGet]
    public IActionResult Index2() => View();


    [HttpPost]
    public IActionResult Index2(Message message)
    {
        CreateNewMessage createNewMessage = new();
        EncryptoBySubjectDecorator encryptoBySubjectDecorator = new(createNewMessage);
        encryptoBySubjectDecorator.SendMessageByEncryptoSubject(message);
        return View();
    }


    [HttpGet]
    public IActionResult Index3() => View();


    [HttpPost]
    public IActionResult Index3(Message message)
    {
        CreateNewMessage createNewMessage = new();
        SubjectIDDecorator subjectIDDecorator = new(createNewMessage);
        subjectIDDecorator.SendMessageIDSubject(message);
        return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}
