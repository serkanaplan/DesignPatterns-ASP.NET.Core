# Observer Tasarım Deseni - ASP.NET Core Örneği

## Giriş

Observer (Gözlemci) tasarım deseni, bir nesnenin durumundaki değişiklikleri diğer nesnelere otomatik olarak bildirmesini sağlayan davranışsal bir desendir. Bu desen, bir-çok ilişkisi kurar; bir nesne (subject) değiştiğinde, ona bağlı tüm nesneler (observers) otomatik olarak bilgilendirilir ve güncellenir.

## Senaryo

Bir haber ajansı sistemi geliştirdiğimizi düşünelim. Bu sistemde, haber ajansı yeni haberler yayınladığında, farklı haber kanalları ve uygulamalar bu haberleri anında almalıdır. Observer desenini kullanarak bu sistemi oluşturacağız.

## Uygulama

1. Öncelikle, Observer arayüzünü ve Subject (Konu) sınıfını tanımlayalım:

```csharp
public interface IObserver
{
    void Update(string news);
}

public class NewsAgency
{
    private List<IObserver> _observers = new List<IObserver>();
    private string _latestNews;

    public void Attach(IObserver observer)
    {
        _observers.Add(observer);
    }

    public void Detach(IObserver observer)
    {
        _observers.Remove(observer);
    }

    public void NotifyObservers()
    {
        foreach (var observer in _observers)
        {
            observer.Update(_latestNews);
        }
    }

    public void SetNews(string news)
    {
        _latestNews = news;
        NotifyObservers();
    }
}
```

2.Şimdi, farklı haber kanalları için concrete observer sınıfları oluşturalım:

```csharp
public class NewsChannel : IObserver
{
    public string Name { get; set; }

    public NewsChannel(string name)
    {
        Name = name;
    }

    public void Update(string news)
    {
        Console.WriteLine($"{Name} yayınlıyor: {news}");
    }
}

public class NewsApp : IObserver
{
    public string AppName { get; set; }

    public NewsApp(string appName)
    {
        AppName = appName;
    }

    public void Update(string news)
    {
        Console.WriteLine($"{AppName} kullanıcılarına bildirim gönderiliyor: {news}");
    }
}
```

3. QASP.NET Core Controller'ında kullanım örneği:

```csharp
[ApiController]
[Route("api/[controller]")]
public class NewsController : ControllerBase
{
    private static NewsAgency _newsAgency = new NewsAgency();

    public NewsController()
    {
        // Observer'ları oluştur ve ekle
        var cnnTurk = new NewsChannel("CNN Türk");
        var ntv = new NewsChannel("NTV");
        var haberApp = new NewsApp("Haber Uygulaması");

        _newsAgency.Attach(cnnTurk);
        _newsAgency.Attach(ntv);
        _newsAgency.Attach(haberApp);
    }

    [HttpPost("publish")]
    public IActionResult PublishNews([FromBody] string news)
    {
        _newsAgency.SetNews(news);
        return Ok("Haber yayınlandı ve tüm kanallara bildirildi.");
    }
}
```
## Nasıl Çalışır?

- Haber ajansı (NewsAgency) yeni bir haber yayınlar.
- SetNews metodu çağrılır ve yeni haber kaydedilir.
- NotifyObservers metodu otomatik olarak çağrılır.
- Tüm kayıtlı gözlemcilerin (NewsChannel ve NewsApp nesneleri) Update metodu çağrılır.
- Her gözlemci, kendi özel uygulamasına göre haberi işler ve yayınlar.

# Avantajları

Loosely Coupled: Subject ve Observer'lar arasında gevşek bir bağlantı vardır.
Açık/Kapalı Prensibi: Yeni observer'lar kolayca eklenebilir, mevcut kodu değiştirmeden.
Dinamik İlişkiler: Observer'lar çalışma zamanında eklenip çıkarılabilir.

# Dezavantajları

Performans Etkisi: Çok sayıda observer varsa, hepsini güncellemek zaman alabilir.
Beklenmeyen Güncellemeler: Observer'lar, değişikliklerin sırasından haberdar olmayabilir.

# Sonuç
Observer deseni, özellikle bir nesnenin durumundaki değişikliklerin birden fazla nesneyi etkilediği durumlarda çok kullanışlıdır. Bu örnekte gördüğümüz gibi, haber ajansı sisteminde, yeni bir haber yayınlandığında tüm ilgili kanallar ve uygulamalar otomatik olarak bilgilendirilir. Bu, sistemin esnekliğini artırır ve yeni haber kanallarının veya uygulamaların kolayca eklenmesine olanak tanır.