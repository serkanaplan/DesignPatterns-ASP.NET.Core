using DecoratorDesignPattern.DAL;

namespace DecoratorDesignPattern.DecoratorPattern;

public class CreateNotifier : INotifier
{
    private readonly Context context = new();
    public void CreateNotify(Notifier notifier)
    {
        context.Notifiers.Add(notifier);
        context.SaveChanges();
    }
}
