using ObserverDesignPattern.DAL;

namespace ObserverDesignPattern.ObserverPattern;

public class CreateWelcomeMessage(IServiceProvider serviceProvider) : IObserver
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    readonly Context context = new();

    public void CreateNewUser(AppUser appUser)
    {
        context.WelcomeMessages.Add(new WelcomeMessage
        {
            NameSurname = appUser.Name + " " + appUser.Surname,
            Content = "Dergi Bültenimize Kayıt Olduğunuz İçin Çok Teşekkür Ederiz, Dergilerimize Web Sitemizden Ulaşabilirsiniz"
        });
        context.SaveChanges();
    }
}
