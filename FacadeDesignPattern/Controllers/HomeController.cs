﻿using FacadeDesignPattern.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FacadeDesignPattern.Controllers;

public class HomeController : Controller
{

    public IActionResult Index() => View();

    public IActionResult GetPrivacy() => View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}