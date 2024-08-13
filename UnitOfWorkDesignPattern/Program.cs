using Microsoft.EntityFrameworkCore;
using UnitOfWorkDesignPattern.BusinessLayer.Abstract;
using UnitOfWorkDesignPattern.BusinessLayer.Concrete;
using UnitOfWorkDesignPattern.DataAccessLayer.Abstract;
using UnitOfWorkDesignPattern.DataAccessLayer.Concrete;
using UnitOfWorkDesignPattern.DataAccessLayer.EntityFramework;
using UnitOfWorkDesignPattern.DataAccessLayer.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ICustomerDal, EfCustomerDal>();
builder.Services.AddScoped<ICustomerService, CustomerManager>();
builder.Services.AddScoped<IUowDal, UowDal>();

builder.Services.AddEntityFrameworkNpgsql().AddDbContext<Context>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
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

app.MapControllerRoute( name: "default",pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
