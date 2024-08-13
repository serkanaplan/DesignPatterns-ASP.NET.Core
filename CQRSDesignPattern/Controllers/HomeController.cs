using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CQRSDesignPattern.Models;
using CQRSDesignPattern.CQRSPattern.Commands;
using CQRSDesignPattern.CQRSPattern.Queries;
using CQRSDesignPattern.CQRSPattern.Handlers;

namespace CQRSDesignPattern.Controllers;

public class HomeController(GetProductQueryHandler getProductQueryHandler, CreateProductCommandHandler createProductCommandHandler, GetProductByIDQueryHandler getProductByIDQueryHandler, RemoveProductCommandHandler removeProductCommandHandler, GetProductUpdateByIDQueryHandler getProductUpdateByIDQueryHandler, UpdateProductCommandHandler updateProductCommandHandler) : Controller
{
    private readonly GetProductQueryHandler _getProductQueryHandler = getProductQueryHandler;
    private readonly CreateProductCommandHandler _createProductCommandHandler = createProductCommandHandler;
    private readonly GetProductByIDQueryHandler _getProductByIDQueryHandler = getProductByIDQueryHandler;
    private readonly RemoveProductCommandHandler _removeProductCommandHandler = removeProductCommandHandler;
    private readonly GetProductUpdateByIDQueryHandler _getProductUpdateByIDQueryHandler = getProductUpdateByIDQueryHandler;
    private readonly UpdateProductCommandHandler _updateProductCommandHandler = updateProductCommandHandler;

    public IActionResult Index()
    {
        var values = _getProductQueryHandler.Handle();
        return View(values);
    }


    [HttpGet]
    public IActionResult AddProduct() => View();


    [HttpPost]
    public IActionResult AddProduct(CreateProductCommand command)
    {
        _createProductCommandHandler.Handle(command);
        return RedirectToAction("Index");
    }


    public IActionResult GetProduct(int id)
    {
        var values = _getProductByIDQueryHandler.Handle(new GetProductByIDQuery(id));
        return View(values);
    }


    public IActionResult DeleteProduct(int id)
    {
        _removeProductCommandHandler.Handle(new RemoveProductCommand(id));
        return RedirectToAction("Index");
    }


    [HttpGet]
    public IActionResult UpdateProduct(int id)
    {
        var values = _getProductUpdateByIDQueryHandler.Handle(new GetProductUpdateByIDQuery(id));
        return View(values);
    }


    [HttpPost]
    public IActionResult UpdateProduct(UpdateProductCommand command)
    {
        _updateProductCommandHandler.Handle(command);
        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}
