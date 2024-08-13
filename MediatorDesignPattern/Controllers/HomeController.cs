using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MediatorDesignPattern.Models;
using MediatR;
using MediatorDesignPattern.MediatorPattern.Queries;
using MediatorDesignPattern.MediatorPattern.Commands;

namespace MediatorDesignPattern.Controllers;

public class HomeController(IMediator mediator) : Controller
{
    private readonly IMediator _mediator = mediator;

    public async Task<IActionResult> Index()
    {
        var values = await _mediator.Send(new GetlAllProductQuery());
        return View(values);
    }

    
    public async Task<IActionResult> GetProduct(int id)
    {
        var values = await _mediator.Send(new GetProductByIDQuery(id));
        return View(values);
    }


    public async Task<IActionResult> DeleteProduct(int id)
    {
        await _mediator.Send(new RemoveProductCommand(id));
        return RedirectToAction("Index");
    }

    
    [HttpGet]
    public async Task<IActionResult> UpdateProduct(int id)
    {
        var values = await _mediator.Send(new GetProductUpdateByIDQuery(id));
        return View(values);
    }


    [HttpPost]
    public async Task<IActionResult> UpdateProduct(UpdateProductCommand updateProductCommand)
    {
        await _mediator.Send(updateProductCommand);
        return RedirectToAction("Index");
    }


    [HttpGet]
    public IActionResult AddProduct() => View();


    [HttpPost]
    public async Task<IActionResult> AddProduct(CreateProductCommand createProductCommand)
    {
        await _mediator.Send(createProductCommand);
        return RedirectToAction("Index");
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}
