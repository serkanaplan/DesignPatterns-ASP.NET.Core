using DecoratorDesignPattern.DAL;

namespace DecoratorDesignPattern.DecoratorPattern2;

public class EncryptByContentDecorator(ISendMessage sendMessage) : Decorator(sendMessage)
{
    private readonly ISendMessage _sendMessage = sendMessage;
    readonly Context context = new();

    public void SendMessageByEncryptoContent(Message message)
    {
        message.MessageSender = "Takım Lideri";
        message.MessageReceiver = "Yazılım Ekibi";
        message.MessageContent = "Saat 17:00'de Publish Yapılacak";
        message.MessageSubject = "Publish";
        string data = "";
        data = message.MessageContent;
        char[] chars = data.ToCharArray();
        foreach (var item in chars)
        {
            message.MessageContent += Convert.ToChar(item + 3).ToString();
        }
        context.Messages.Add(message);
        context.SaveChanges();
    }
    public override void SendMessage(Message message)
    {
        base.SendMessage(message);
        SendMessageByEncryptoContent(message);
    }
}
