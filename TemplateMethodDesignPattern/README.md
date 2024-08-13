# Template Method Tasarım Deseni - ASP.NET Core Örneği

## Giriş

Template Method tasarım deseni, bir algoritmanın iskeletini tanımlayan ancak bazı adımların uygulanmasını alt sınıflara bırakan davranışsal bir tasarım desenidir. Bu desen, bir algoritmanın yapısını değiştirmeden belirli adımlarını yeniden tanımlamamıza olanak tanır.

## Senaryo

Bir e-ticaret platformu için farklı ödeme yöntemlerini işleyen bir sistem geliştirdiğimizi düşünelim. Her ödeme yöntemi (kredi kartı, PayPal, banka transferi) benzer adımları izler, ancak her birinin kendine özgü detayları vardır. Template Method desenini kullanarak bu süreci standartlaştıracağız.

## Uygulama

1. Öncelikle, ödeme işlemini temsil eden soyut bir sınıf oluşturalım:

```csharp
public abstract class PaymentProcessor
{
    // Template Method
    public void ProcessPayment(decimal amount)
    {
        ValidatePayment(amount);
        CollectPaymentDetails();
        if (IsCustomerValidated())
        {
            ExecutePayment(amount);
            SendConfirmation();
        }
        else
        {
            HandleFailedValidation();
        }
    }

    protected abstract void ValidatePayment(decimal amount);
    protected abstract void CollectPaymentDetails();
    protected abstract bool IsCustomerValidated();
    protected abstract void ExecutePayment(decimal amount);

    // Hook method
    protected virtual void SendConfirmation()
    {
        Console.WriteLine("Generic payment confirmation sent.");
    }

    private void HandleFailedValidation()
    {
        Console.WriteLine("Customer validation failed. Payment cancelled.");
    }
}
```

2. Şimdi, farklı ödeme yöntemleri için somut sınıflar oluşturalım:

```csharp
public class CreditCardPaymentProcessor : PaymentProcessor
{
    protected override void ValidatePayment(decimal amount)
    {
        Console.WriteLine($"Validating credit card payment of {amount:C}");
    }

    protected override void CollectPaymentDetails()
    {
        Console.WriteLine("Collecting credit card details");
    }

    protected override bool IsCustomerValidated()
    {
        Console.WriteLine("Validating credit card customer");
        return true;
    }

    protected override void ExecutePayment(decimal amount)
    {
        Console.WriteLine($"Processing credit card payment of {amount:C}");
    }

    protected override void SendConfirmation()
    {
        Console.WriteLine("Sending credit card payment confirmation email");
    }
}

public class PayPalPaymentProcessor : PaymentProcessor
{
    protected override void ValidatePayment(decimal amount)
    {
        Console.WriteLine($"Validating PayPal payment of {amount:C}");
    }

    protected override void CollectPaymentDetails()
    {
        Console.WriteLine("Collecting PayPal account details");
    }

    protected override bool IsCustomerValidated()
    {
        Console.WriteLine("Validating PayPal customer");
        return true;
    }

    protected override void ExecutePayment(decimal amount)
    {
        Console.WriteLine($"Processing PayPal payment of {amount:C}");
    }
}

public class BankTransferPaymentProcessor : PaymentProcessor
{
    protected override void ValidatePayment(decimal amount)
    {
        Console.WriteLine($"Validating bank transfer payment of {amount:C}");
    }

    protected override void CollectPaymentDetails()
    {
        Console.WriteLine("Collecting bank account details");
    }

    protected override bool IsCustomerValidated()
    {
        Console.WriteLine("Validating bank account customer");
        return true;
    }

    protected override void ExecutePayment(decimal amount)
    {
        Console.WriteLine($"Processing bank transfer payment of {amount:C}");
    }
}
```

3. ASP.NET Core Controller'ında kullanım örneği:

```csharp
[ApiController]
[Route("api/[controller]")]
public class PaymentController : ControllerBase
{
    [HttpPost("creditcard")]
    public IActionResult ProcessCreditCardPayment([FromBody] decimal amount)
    {
        var processor = new CreditCardPaymentProcessor();
        processor.ProcessPayment(amount);
        return Ok("Credit card payment processed successfully");
    }

    [HttpPost("paypal")]
    public IActionResult ProcessPayPalPayment([FromBody] decimal amount)
    {
        var processor = new PayPalPaymentProcessor();
        processor.ProcessPayment(amount);
        return Ok("PayPal payment processed successfully");
    }

    [HttpPost("banktransfer")]
    public IActionResult ProcessBankTransferPayment([FromBody] decimal amount)
    {
        var processor = new BankTransferPaymentProcessor();
        processor.ProcessPayment(amount);
        return Ok("Bank transfer payment processed successfully");
    }
}
```

# Nasıl Çalışır?

- Kullanıcı bir ödeme isteği gönderir (kredi kartı, PayPal veya banka transferi).
- Controller, isteğin türüne göre uygun PaymentProcessor nesnesini oluşturur.
- ProcessPayment metodu çağrılır, bu metod tüm ödeme yöntemleri için ortak olan adımları içerir.
- Her bir somut sınıf (CreditCardPaymentProcessor, PayPalPaymentProcessor, BankTransferPaymentProcessor), kendi özel uygulamalarını sağlar.
- Ödeme işlemi tamamlanır ve sonuç kullanıcıya döndürülür.

## Avantajları

Kod tekrarını azaltır: Ortak adımlar ana sınıfta tanımlanır.
Esneklik sağlar: Yeni ödeme yöntemleri kolayca eklenebilir.
Değişikliklere açıktır: Algoritmanın yapısını bozmadan belirli adımlar değiştirilebilir.
İş mantığının tutarlılığını sağlar: Tüm ödeme yöntemleri aynı temel adımları izler.

## Dezavantajları

Kalıtım kullanıldığı için, aşırı kullanımı tasarımı karmaşıklaştırabilir.
Alt sınıflar, üst sınıfın yapısına bağımlı hale gelebilir.

## Sonuç
Template Method deseni, özellikle benzer işlemleri farklı şekillerde gerçekleştiren senaryolarda çok kullanışlıdır. Bu örnekte gördüğümüz gibi, farklı ödeme yöntemlerini standart bir yapıda işlerken her birinin özel gereksinimlerini de karşılayabiliyoruz. Bu, kodun bakımını kolaylaştırır ve yeni ödeme yöntemlerinin eklenmesini basitleştirir.