using DecoratorDesignPattern.DAL;

namespace DecoratorDesignPattern.DecoratorPattern2;

public class SubjectIDDecorator(ISendMessage sendMessage) : Decorator(sendMessage)
{
    private readonly ISendMessage _sendMessage = sendMessage;
    readonly Context context = new();

    public void SendMessageIDSubject(Message message)
    {
        if (message.MessageSubject == "1")
        {
            message.MessageSubject = "Toplantı";
        }
        if (message.MessageSubject == "2")
        {
            message.MessageSubject = "Scrum Toplantısı";
        }
        if (message.MessageSubject == "3")
        {
            message.MessageSubject = "Haftalık Değerlendirme";
        }
        context.Messages.Add(message);
        context.SaveChanges();
    }
    public override void SendMessage(Message message)
    {
        base.SendMessage(message);
        SendMessageIDSubject(message);
    }
}
