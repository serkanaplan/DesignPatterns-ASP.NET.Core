using DecoratorDesignPattern.DAL;

namespace DecoratorDesignPattern.DecoratorPattern2;

public class Decorator(ISendMessage sendMessage) : ISendMessage
{
    private readonly ISendMessage _sendMessage = sendMessage;

    virtual public void SendMessage(Message message)
    {
        message.MessageReceiver = "Everybody";
        message.MessageSender = "Admin";
        message.MessageContent = "Merhaba bu bir toplantı mesajıdır";
        message.MessageSubject = "Toplantı";
        _sendMessage.SendMessage(message);
    }
}
