using ObserverDesignPattern.DAL;

namespace ObserverDesignPattern.ObserverPattern;

public class CreateMagazineAnnocuncement(IServiceProvider serviceProvider) : IObserver
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    readonly Context context = new();

    public void CreateNewUser(AppUser appUser)
    {
        context.UserProcesses.Add(new UserProcess
        {
            NameSurname = appUser.Name + " " + appUser.Surname,
            Magazine = "Bilim Dergisi",
            Content = "Bilim Dergimizin Mart Sayısı 1 Martta evinize ulaştırılacaktır, konular Jupiter Gezegeni ve Mars olacaktır"
        });
        context.SaveChanges();
    }
}
