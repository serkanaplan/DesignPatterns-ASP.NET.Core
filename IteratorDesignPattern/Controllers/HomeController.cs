using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using IteratorDesignPattern.Models;
using IteratorDesignPattern.IteratorPattern;

namespace IteratorDesignPattern.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
        {
            VisitRouteMover visitRouteMover = new VisitRouteMover();
            List<string> strings = [];

            visitRouteMover.AddVisitRoute(new VisitRoute { CountryName = "Almanya", CityName = "Berlin", VisitPlaceName = "Berlin Kapısı" });
            visitRouteMover.AddVisitRoute(new VisitRoute { CountryName = "Fransa", CityName = "Paris", VisitPlaceName = "Eyfel" });
            visitRouteMover.AddVisitRoute(new VisitRoute { CountryName = "İtalya", CityName = "Venedik", VisitPlaceName = "Gondol" });
            visitRouteMover.AddVisitRoute(new VisitRoute { CountryName = "İtalya", CityName = "Roma", VisitPlaceName = "Kolezyum" });
            visitRouteMover.AddVisitRoute(new VisitRoute { CountryName = "Çek Cumhuriyeti", CityName = "Prag", VisitPlaceName = "Meydan" });

            var iterator = visitRouteMover.CreateIterator();

            while (iterator.NextLocation())
            {
                strings.Add(iterator.CurrentItem.CountryName + " " + iterator.CurrentItem.CityName + " " + iterator.CurrentItem.VisitPlaceName);
            }

            ViewBag.v = strings;

            return View();
        }



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}
