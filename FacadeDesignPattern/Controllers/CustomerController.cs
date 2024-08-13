using FacadeDesignPattern.DAL;
using Microsoft.AspNetCore.Mvc;

namespace DesignPattern.Facade.Controllers
{
    public class CustomerController : Controller
    {
        readonly Context context = new();

        [HttpGet]
        public IActionResult AddNewCustomer() => View();


        [HttpPost]
        public IActionResult AddNewCustomer(Customer customer)
        {
            context.Customers.Add(customer);
            context.SaveChanges();
            return RedirectToAction("CustomerList");
        }

        
        public IActionResult CustomerList()
        {
            var values = context.Customers.ToList();
            return View(values);
        }
    }
}
