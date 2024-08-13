# Chain of Responsibility Tasarım Deseni - ASP.NET Core Örneği

## Giriş

Chain of Responsibility (Sorumluluk Zinciri) tasarım deseni, bir isteği işlemek için birden fazla nesnenin sırayla denenmesini sağlar. Bu desen, istekleri işleyen nesneler arasında gevşek bir bağlantı oluşturur ve her bir nesnenin kendi sorumluluğunu bilmesine olanak tanır.

## Senaryo

Bir e-ticaret web sitesi için müşteri destek sistemi geliştirdiğimizi düşünelim. Müşteriler farklı konularda destek talepleri gönderebilirler. Bu talepleri uygun departmanlara yönlendirmek için Chain of Responsibility desenini kullanacağız.

## Uygulama

1. Öncelikle, tüm destek taleplerini temsil eden bir sınıf oluşturalım:

```csharp
public class SupportTicket
{
    public string CustomerName { get; set; }
    public string Issue { get; set; }
}
```

2. Şimdi, destek taleplerini işleyecek soyut bir sınıf oluşturalım:

```csharp
abstract class SupportHandler
{
    protected SupportHandler nextHandler;

    public void SetNextHandler(SupportHandler handler)
    {
        nextHandler = handler;
    }

    public abstract void HandleRequest(SupportTicket ticket);
}
```

3. Farklı departmanlar için somut işleyici sınıflar oluşturalım:

```csharp
public class GeneralSupportHandler : SupportHandler
{
    public override void HandleRequest(SupportTicket ticket)
    {
        if (ticket.Issue.Contains("genel"))
        {
            Console.WriteLine($"Genel Destek, {ticket.CustomerName}'in talebini işliyor.");
        }
        else if (nextHandler != null)
        {
            nextHandler.HandleRequest(ticket);
        }
    }
}

public class TechnicalSupportHandler : SupportHandler
{
    public override void HandleRequest(SupportTicket ticket)
    {
        if (ticket.Issue.Contains("teknik"))
        {
            Console.WriteLine($"Teknik Destek, {ticket.CustomerName}'in talebini işliyor.");
        }
        else if (nextHandler != null)
        {
            nextHandler.HandleRequest(ticket);
        }
    }
}

public class BillingSupportHandler : SupportHandler
{
    public override void HandleRequest(SupportTicket ticket)
    {
        if (ticket.Issue.Contains("fatura"))
        {
            Console.WriteLine($"Fatura Desteği, {ticket.CustomerName}'in talebini işliyor.");
        }
        else if (nextHandler != null)
        {
            nextHandler.HandleRequest(ticket);
        }
    }
}
```

4. ASP.NET Core Controller'ında kullanım örneği:

```csharp
[ApiController]
[Route("api/[controller]")]
public class SupportController : ControllerBase
{
    private readonly SupportHandler _supportChain;

    public SupportController()
    {
        // Destek zincirini oluştur
        _supportChain = new GeneralSupportHandler();
        var technicalSupport = new TechnicalSupportHandler();
        var billingSupport = new BillingSupportHandler();

        _supportChain.SetNextHandler(technicalSupport);
        technicalSupport.SetNextHandler(billingSupport);
    }

    [HttpPost]
    public IActionResult SubmitTicket([FromBody] SupportTicket ticket)
    {
        _supportChain.HandleRequest(ticket);
        return Ok("Destek talebiniz işleme alındı.");
    }
}
```


# Nasıl Çalışır?

- 1.Müşteri bir destek talebi gönderir.
- 2.Talep, zincirin ilk halkası olan Genel Destek'e iletilir.
- 3.Eğer Genel Destek talebi işleyemezse, talep Teknik Destek'e iletilir.
- 4.Eğer Teknik Destek de talebi işleyemezse, talep Fatura Desteği'ne iletilir.
- 5.Her bir handler, talebi işleyip işleyemeyeceğine karar verir. İşleyemezse, bir sonraki handler'a iletir.

## Avantajları

Yeni destek türleri eklemek kolaydır. Sadece yeni bir handler sınıfı oluşturup zincire eklersiniz.
Her bir handler'ın sorumluluğu açıkça belirlenmiştir.
İsteklerin işlenme sırası değiştirilebilir.

## Sonuç
Chain of Responsibility deseni, karmaşık karar verme süreçlerini basitleştirir ve kodun esnekliğini artırır. Bu örnekte gördüğümüz gibi, farklı türdeki destek taleplerini uygun departmanlara yönlendirmek için etkili bir çözüm sunar.