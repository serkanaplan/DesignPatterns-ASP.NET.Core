using DecoratorDesignPattern.DAL;

namespace DecoratorDesignPattern.DecoratorPattern;

public class MailDecorator(INotifier notifier) : Decorator(notifier)
{
    private readonly INotifier _notifier = notifier;
    readonly Context context = new();

    public void SendMailNotify(Notifier notifier)
    {
        notifier.NotifierSubject = "Günlük Sabah Toplantısı";
        notifier.NotifierCreator = "Scrum Master";
        notifier.NotifierChannel = "GMail-Outlook";
        notifier.NotifierType = "Private Team";
        context.Notifiers.Add(notifier);
        context.SaveChanges();
    }
    public override void CreateNotify(Notifier notifier)
    {
        base.CreateNotify(notifier);
        SendMailNotify(notifier);
    }
}
