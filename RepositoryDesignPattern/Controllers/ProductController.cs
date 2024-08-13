﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RepositoryDesignPattern.BusinessLayer.Abstract;
using RepositoryDesignPattern.EntityLayer;

namespace RepositoryDesignPattern.Controllers;

public class ProductController(IProductService productService, ICategoryService categoryService) : Controller
{
    private readonly IProductService _productService = productService;
    private readonly ICategoryService _categoryService = categoryService;


    public IActionResult Index()
    {
        var values = _productService.TGetList();
        return View(values);
    }


    public IActionResult Index2()
    {
        var values = _productService.TProductListWithCategory();
        return View(values);
    }
    

    [HttpGet]
    public IActionResult AddProduct()
    {
        List<SelectListItem> values = (from x in _categoryService.TGetList() select new SelectListItem
                                       {
                                           Text = x.CategoryName,
                                           Value = x.CategoryID.ToString()
                                       }).ToList();
        ViewBag.v = values;
        return View();
    }


    [HttpPost]
    public IActionResult AddProduct(Product product)
    {
        _productService.TInsert(product);
        return RedirectToAction("Index");
    }


    public IActionResult DeleteProduct(int id)
    {
        var value = _productService.TGetByID(id);
        _productService.TDelete(value);
        return RedirectToAction("Index");
    }


    [HttpGet]
    public IActionResult UpdateProduct(int id)
    {
        List<SelectListItem> values = (from x in _categoryService.TGetList()
                                       select new SelectListItem
                                       {
                                           Text = x.CategoryName,
                                           Value = x.CategoryID.ToString()
                                       }).ToList();
        ViewBag.v = values;
        var value = _productService.TGetByID(id);
        return View(value);
    }

    
    [HttpPost]
    public IActionResult UpdateProduct(Product product)
    {
        _productService.TUpdate(product);
        return RedirectToAction("Index");
    }
}
