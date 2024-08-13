using ObserverDesignPattern.DAL;

namespace ObserverDesignPattern.ObserverPattern;

public interface IObserver
{
    void CreateNewUser(AppUser appUser);
}
