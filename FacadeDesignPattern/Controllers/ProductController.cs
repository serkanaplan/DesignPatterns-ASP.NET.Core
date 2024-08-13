using FacadeDesignPattern.DAL;
using Microsoft.AspNetCore.Mvc;

namespace FacadeDesignPattern.Controllers;

public class ProductController : Controller
{
    readonly Context context = new();

    [HttpGet]
    public IActionResult AddNewProduct() => View();


    [HttpPost]
    public IActionResult AddNewProduct(Product product)
    {
        context.Products.Add(product);
        context.SaveChanges();
        return RedirectToAction("ProductList");
    }

    
    public IActionResult ProductList()
    {
        var values = context.Products.ToList();
        return View(values);
    }
}
