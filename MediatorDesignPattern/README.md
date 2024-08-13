# Mediator Design Pattern - ASP.NET Core MVC Örneği

Bu proje, **Mediator Design Pattern** kullanarak ASP.NET Core MVC ile basit bir senaryoyu göstermektedir. Mediator deseni, bir grup nesnenin birbiriyle etkileşimde bulunması gerektiğinde kullanılır, ancak bu etkileşimlerin karmaşıklaşmasını önlemek için doğrudan iletişim yerine merkezi bir "arabulucu" üzerinden iletişim sağlanır.

## Proje Yapısı

/MediatorPatternExample
│
├── Controllers
│ ├── HomeController.cs
│ └── OrderController.cs
│
├── Mediator
│ ├── IMediator.cs
│ ├── OrderMediator.cs
│ ├── IRequest.cs
│ └── IResponse.cs
│
├── Models
│ └── Order.cs
│
└── Views
├── Home
│ └── Index.cshtml
└── Order
└── OrderSummary.cshtml


## Senaryo

Bir alışveriş senaryosunu ele alalım: Kullanıcı bir sipariş verir ve sipariş onaylanır. Bu süreçte kullanıcı, ürünlerin sepete eklenmesi, siparişin tamamlanması ve sipariş özetinin görüntülenmesi gibi adımları takip eder. Bu işlemler arasında koordinasyonu sağlamak için Mediator tasarım deseni kullanılır.

### Adımlar:

1. **Order (Sipariş) Modeli**: Siparişin temel özelliklerini içerir.
2. **IMediator Arayüzü**: Mediator'ün temel fonksiyonlarını tanımlar.
3. **OrderMediator Sınıfı**: IMediator'ü implement eden sınıf, sipariş sürecindeki adımları koordine eder.
4. **IRequest ve IResponse Arayüzleri**: İstek ve yanıt yapıları için kullanılan temel arayüzlerdir.
5. **OrderController**: Siparişin oluşturulmasını ve özetlenmesini sağlar.
6. **Viewlar**: Kullanıcıya sunulan arayüzleri içerir.

## Kod Örnekleri

### Order.cs (Model)

```csharp
public class Order
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
```
# IMediator.cs (Arayüz)

```cs
public interface IMediator
{
    IResponse Handle(IRequest request);
}
```
# IRequest.cs ve IResponse.cs

```cs
public interface IRequest { }

public interface IResponse 
{
    bool Success { get; set; }
}
```

# OrderMediator.cs (Mediator Sınıfı)

```cs
public class OrderMediator : IMediator
{
    public IResponse Handle(IRequest request)
    {
        // İşlemleri koordine edin
        var response = new OrderResponse { Success = true };
        
        // İşlemler burada yürütülebilir
        // Örn: Sipariş ver, stok kontrolü yap, ödeme işlemi başlat vb.

        return response;
    }
}

public class OrderRequest : IRequest
{
    public Order Order { get; set; }
}

public class OrderResponse : IResponse
{
    public bool Success { get; set; }
}
```

# OrderController.cs

```cs
public class OrderController : Controller
{
    private readonly IMediator _mediator;

    public OrderController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public IActionResult CreateOrder(Order order)
    {
        var request = new OrderRequest { Order = order };
        var response = _mediator.Handle(request);

        if (response.Success)
        {
            return RedirectToAction("OrderSummary", order);
        }
        return View("Error");
    }

    public IActionResult OrderSummary(Order order)
    {
        return View(order);
    }
}
```

# Dependency Injection
- Mediator sınıfının Dependency Injection (DI) ile projeye eklenmesi:

```cs
public void ConfigureServices(IServiceCollection services)
{
    services.AddControllersWithViews();
    services.AddScoped<IMediator, OrderMediator>();
}
```

# Kullanım
- Projeyi çalıştırın ve bir sipariş oluşturun.
- Sipariş oluşturulduktan sonra, Mediator bu siparişin işlenmesini koordine eder.
- Sipariş başarıyla işlenirse, sipariş özeti görüntülenir.

# Sonuç
Bu örnek, Mediator Design Pattern'in ASP.NET Core MVC projelerinde nasıl kullanılabileceğini göstermektedir. Bu desen, kodunuzu daha temiz ve bakımı kolay hale getirmeye yardımcı olur.