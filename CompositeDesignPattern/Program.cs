
using CompositeDesignPattern.Data;
using CompositeDesignPattern.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (!context.Categories.Any())
    {
        var electronics = new Category("Electronics");
        var phones = new Category("Phones");
        var samsung = new Product("Samsung Galaxy S21");
        var iphone = new Product("iPhone 12");

        phones.Add(samsung);
        phones.Add(iphone);
        electronics.Add(phones);

        context.Categories.Add(electronics);
        context.SaveChanges();
    }
}
app.Run();
