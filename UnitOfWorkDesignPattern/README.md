# Unit of Work Tasarım Deseni - ASP.NET Core Örneği

## Giriş

Unit of Work (İş Birimi) tasarım deseni, bir iş birimi sırasında yapılan tüm veritabanı işlemlerini gruplandıran ve bu işlemlerin tutarlı bir şekilde uygulanmasını sağlayan bir desendir. Bu desen, veritabanı işlemlerinin yönetimini merkezileştirir ve transaction yönetimini kolaylaştırır.

## Senaryo

Bir e-ticaret uygulaması geliştirdiğimizi düşünelim. Bu uygulamada, bir sipariş oluşturulduğunda hem sipariş tablosuna yeni bir kayıt eklenmeli, hem de ürün stoklarının güncellenmesi gerekiyor. Unit of Work deseni, bu işlemlerin tek bir transaction içinde gerçekleştirilmesini sağlar.

## Uygulama

1. İlk olarak, IRepository ve IUnitOfWork arayüzlerini tanımlayalım:

```csharp
public interface IRepository<T> where T : class
{
    T GetById(int id);
    IEnumerable<T> GetAll();
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
}

public interface IUnitOfWork : IDisposable
{
    IRepository<Order> Orders { get; }
    IRepository<Product> Products { get; }
    int Complete();
}
```

2. Şimdi, bu arayüzlerin uygulamalarını yapalım:

```csharp
public class Repository<T> : IRepository<T> where T : class
{
    protected readonly DbContext Context;

    public Repository(DbContext context)
    {
        Context = context;
    }

    public T GetById(int id)
    {
        return Context.Set<T>().Find(id);
    }

    public IEnumerable<T> GetAll()
    {
        return Context.Set<T>().ToList();
    }

    public void Add(T entity)
    {
        Context.Set<T>().Add(entity);
    }

    public void Update(T entity)
    {
        Context.Set<T>().Update(entity);
    }

    public void Delete(T entity)
    {
        Context.Set<T>().Remove(entity);
    }
}

public class UnitOfWork : IUnitOfWork
{
    private readonly DbContext _context;
    public IRepository<Order> Orders { get; private set; }
    public IRepository<Product> Products { get; private set; }

    public UnitOfWork(DbContext context)
    {
        _context = context;
        Orders = new Repository<Order>(_context);
        Products = new Repository<Product>(_context);
    }

    public int Complete()
    {
        return _context.SaveChanges();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
```

3. ASP.NET Core servis yapılandırması:

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddDbContext<YourDbContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
    
    services.AddScoped<IUnitOfWork, UnitOfWork>();
    // Diğer servis yapılandırmaları...
}
```
4. Controller'da kullanım örneği:

```csharp
[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public OrderController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpPost]
    public IActionResult CreateOrder(OrderDto orderDto)
    {
        var order = new Order
        {
            // OrderDto'dan Order nesnesine dönüşüm
        };

        _unitOfWork.Orders.Add(order);

        var product = _unitOfWork.Products.GetById(orderDto.ProductId);
        if (product != null)
        {
            product.Stock -= orderDto.Quantity;
            _unitOfWork.Products.Update(product);
        }

        _unitOfWork.Complete();

        return Ok("Sipariş başarıyla oluşturuldu.");
    }
}
```
## Nasıl Çalışır?

- UnitOfWork nesnesi, tüm repository'leri içerir ve DbContext'i yönetir.
- Her bir repository, belirli bir entity tipi için CRUD işlemlerini gerçekleştirir.
- Controller'da UnitOfWork kullanılarak işlemler gerçekleştirilir.
- Tüm işlemler tamamlandığında, UnitOfWork'ün Complete metodu çağrılarak değişiklikler veritabanına yansıtılır.

# Avantajları

- İş mantığı ve veritabanı işlemleri arasında bir soyutlama katmanı sağlar.
- Birden fazla veritabanı işlemini tek bir transaction içinde yönetmeyi kolaylaştırır.
- Kod tekrarını azaltır ve bakımı kolaylaştırır.
- Test edilebilirliği artırır, çünkü UnitOfWork ve Repository'ler kolayca mock'lanabilir.

# Dezavantajları

- Küçük uygulamalar için fazla karmaşık olabilir.
- Yanlış kullanıldığında performans sorunlarına yol açabilir (örneğin, gereksiz yere büyük veri kümelerini belleğe yüklemek).

# Sonuç
Unit of Work deseni, özellikle karmaşık iş mantığı ve çoklu veritabanı işlemleri içeren uygulamalarda çok faydalıdır. Bu desen, veritabanı işlemlerinin tutarlılığını sağlar ve kodun organize edilmesine yardımcı olur. Ancak, her uygulama için uygun olmayabilir ve projenin ihtiyaçlarına göre değerlendirilmelidir.