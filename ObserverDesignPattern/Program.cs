using ObserverDesignPattern.DAL;
using ObserverDesignPattern.ObserverPattern;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<Context>();
builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<Context>();
builder.Services.AddSingleton<ObserverObject>(sp =>
{
    ObserverObject observerObject = new();
    observerObject.RegisterObserver(new CreateWelcomeMessage(sp));
    observerObject.RegisterObserver(new CreateMagazineAnnocuncement(sp));
    observerObject.RegisterObserver(new CreateDiscountCode(sp));
    return observerObject;
});

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
app.MapControllerRoute( name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
