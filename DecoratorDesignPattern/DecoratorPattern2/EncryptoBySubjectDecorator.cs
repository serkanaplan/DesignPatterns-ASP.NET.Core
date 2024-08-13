using DecoratorDesignPattern.DAL;

namespace DecoratorDesignPattern.DecoratorPattern2;

public class EncryptoBySubjectDecorator(ISendMessage sendMessage) : Decorator(sendMessage)
{
    private readonly ISendMessage _sendMessage = sendMessage;
    readonly Context context = new();

    public void SendMessageByEncryptoSubject(Message message)
    {
        string data = "";
        data = message.MessageSubject;
        char[] chars = data.ToCharArray();
        foreach (var item in chars)
        {
            message.MessageSubject += Convert.ToChar(item + 3).ToString();
        }
        context.Messages.Add(message);
        context.SaveChanges();
    }
    
    public override void SendMessage(Message message)
    {
        base.SendMessage(message);
        SendMessageByEncryptoSubject(message);
    }
}
//A B C D E
//D E F ...