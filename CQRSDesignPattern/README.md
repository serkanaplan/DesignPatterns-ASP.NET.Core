# CQRS Tasarım Deseni - ASP.NET Core Örneği

## Giriş

CQRS (Command Query Responsibility Segregation), bir sistemin veri okuma (query) ve veri yazma (command) işlemlerini birbirinden ayıran bir tasarım desenidir. Bu desen, karmaşık sistemlerde performans, ölçeklenebilirlik ve bakım kolaylığı sağlar.

## Senaryo

Bir e-ticaret platformu için ürün yönetim sistemi geliştirdiğimizi düşünelim. Bu sistemde ürünleri listelemek, detaylarını görüntülemek, yeni ürün eklemek ve mevcut ürünleri güncellemek istiyoruz. CQRS desenini kullanarak bu işlemleri ayrı ayrı ele alacağız.

## Uygulama

1. Öncelikle, ürünü temsil eden bir model oluşturalım:

```csharp
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
}
```

2.Command (Yazma) işlemleri için sınıflar oluşturalım:

```csharp
public class CreateProductCommand
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
}

public class UpdateProductCommand
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
}

public class DeleteProductCommand
{
    public int Id { get; set; }
}

public interface IProductCommandHandler
{
    void Handle(CreateProductCommand command);
    void Handle(UpdateProductCommand command);
    void Handle(DeleteProductCommand command);
}

public class ProductCommandHandler : IProductCommandHandler
{
    private readonly ProductDbContext _context;

    public ProductCommandHandler(ProductDbContext context)
    {
        _context = context;
    }

    public void Handle(CreateProductCommand command)
    {
        var product = new Product
        {
            Name = command.Name,
            Price = command.Price,
            Stock = command.Stock
        };
        _context.Products.Add(product);
        _context.SaveChanges();
    }

    public void Handle(UpdateProductCommand command)
    {
        var product = _context.Products.Find(command.Id);
        if (product != null)
        {
            product.Name = command.Name;
            product.Price = command.Price;
            product.Stock = command.Stock;
            _context.SaveChanges();
        }
    }

      public void Handle(DeleteProductCommand command)
    {
        var product = _context.Products.Find(command.Id);
        if (product != null)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
        }
    }
}
```

3. Query (Okuma) işlemleri için sınıflar oluşturalım:

```csharp
public class ProductListQuery { }

public class ProductDetailQuery
{
    public int Id { get; set; }
}

public interface IProductQueryHandler
{
    List<Product> Handle(ProductListQuery query);
    Product Handle(ProductDetailQuery query);
}

public class ProductQueryHandler : IProductQueryHandler
{
    private readonly ProductDbContext _context;

    public ProductQueryHandler(ProductDbContext context)
    {
        _context = context;
    }

    public List<Product> Handle(ProductListQuery query)
    {
        return _context.Products.ToList();
    }

    public Product Handle(ProductDetailQuery query)
    {
        return _context.Products.Find(query.Id);
    }
}
```

4. ASP.NET Core Controller'ında kullanım örneği:

```csharp
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductCommandHandler _commandHandler;
    private readonly IProductQueryHandler _queryHandler;

    public ProductsController(IProductCommandHandler commandHandler, IProductQueryHandler queryHandler)
    {
        _commandHandler = commandHandler;
        _queryHandler = queryHandler;
    }

    [HttpGet]
    public IActionResult GetProducts()
    {
        var products = _queryHandler.Handle(new ProductListQuery());
        return Ok(products);
    }

    [HttpGet("{id}")]
    public IActionResult GetProduct(int id)
    {
        var product = _queryHandler.Handle(new ProductDetailQuery { Id = id });
        if (product == null)
            return NotFound();
        return Ok(product);
    }

    [HttpPost]
    public IActionResult CreateProduct([FromBody] CreateProductCommand command)
    {
        _commandHandler.Handle(command);
        return CreatedAtAction(nameof(GetProduct), new { id = 0 }, null);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateProduct(int id, [FromBody] UpdateProductCommand command)
    {
        command.Id = id;
        _commandHandler.Handle(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteProduct(int id)
    {
        _commandHandler.Handle(new DeleteProductCommand { Id = id });
        return NoContent();
    }
}
```


# Nasıl Çalışır?

- Kullanıcı bir istek gönderir (ürün listesi, ürün detayı, yeni ürün ekleme veya güncelleme).
- Controller, isteğin türüne göre uygun Command veya Query nesnesini oluşturur.
- İstek, ilgili handler'a (CommandHandler veya QueryHandler) iletilir.
- Handler, isteği işler ve sonucu döndürür.
- Controller, sonucu kullanıcıya geri gönderir.

## Avantajları

Okuma ve yazma işlemleri ayrıldığı için, her biri bağımsız olarak ölçeklendirilebilir.
Karmaşık sistemlerde performans optimizasyonu yapmak daha kolaydır.
Sorgu ve komut modelleri ayrı olduğu için, iş mantığı daha net bir şekilde organize edilebilir.
Farklı veri depolama stratejileri (örneğin, okuma için bellek içi cache, yazma için veritabanı) kullanılabilir.

## Sonuç
CQRS deseni, özellikle karmaşık ve yüksek trafikli sistemlerde, performans ve ölçeklenebilirlik sorunlarını çözmek için etkili bir yöntemdir. Bu örnekte gördüğümüz gibi, ürün yönetimi gibi temel bir senaryoda bile, okuma ve yazma işlemlerinin ayrılması, sistemin daha modüler ve yönetilebilir olmasını sağlar.