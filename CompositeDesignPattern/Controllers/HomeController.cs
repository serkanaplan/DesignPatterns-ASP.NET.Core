
using System.Diagnostics;
using CompositeDesignPattern.Data;
using CompositeDesignPattern.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompositeDesignPattern.Controllers;

public class HomeController(AppDbContext context) : Controller
{
     private readonly AppDbContext _context = context;

    public IActionResult Index()
    {
        var categories = _context.Categories.Include(c => c.Components).ToList();
        return View(categories);
    }
   
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}
