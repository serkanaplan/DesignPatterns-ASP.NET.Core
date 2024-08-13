
using DecoratorDesignPattern.DAL;

namespace DecoratorDesignPattern.DecoratorPattern;

public class Decorator(INotifier notifier) : INotifier
{
    private readonly INotifier _notifier = notifier;

    virtual public void CreateNotify(Notifier notifier)
    {
        notifier.NotifierCreator = "Admin";
        notifier.NotifierSubject = "Toplantı";
        notifier.NotifierType = "Public";
        notifier.NotifierChannel = "Whatsapp";
        _notifier.CreateNotify(notifier);
    }
}
