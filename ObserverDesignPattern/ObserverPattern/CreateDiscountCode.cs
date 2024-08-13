using ObserverDesignPattern.DAL;

namespace ObserverDesignPattern.ObserverPattern;

public class CreateDiscountCode(IServiceProvider serviceProvider) : IObserver
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    readonly Context context = new();

    public void CreateNewUser(AppUser appUser)
    {
        context.Discounts.Add(new Discount
        {
            DiscountCode = "DERGIMART",
            DiscountAmount = 35,
            DiscountCodeStatus = true
        });
        context.SaveChanges();
    }
}
