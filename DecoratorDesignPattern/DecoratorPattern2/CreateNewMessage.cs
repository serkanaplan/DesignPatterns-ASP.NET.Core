using DecoratorDesignPattern.DAL;

namespace DecoratorDesignPattern.DecoratorPattern2;

public class CreateNewMessage : ISendMessage
{
    readonly Context context = new();
    public void SendMessage(Message message)
    {
        context.Messages.Add(message);
        context.SaveChanges();
    }
}
