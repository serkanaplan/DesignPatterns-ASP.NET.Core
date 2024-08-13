# Repository Tasarım Deseni - ASP.NET Core Örneği

## Giriş

Repository tasarım deseni, veri erişim mantığını soyutlayan ve uygulamanın geri kalanından ayıran bir desendir. Bu desen, veri kaynağı ile iş mantığı arasında bir arayüz görevi görür, böylece veri erişimi daha yönetilebilir ve test edilebilir hale gelir.

## Senaryo

Bir blog uygulaması geliştirdiğimizi düşünelim. Bu uygulamada blog yazılarını ve yorumları yönetmemiz gerekiyor. Repository desenini kullanarak, bu verilere erişimi ve yönetimini organize edeceğiz.

## Uygulama

1. Öncelikle, temel entity sınıflarımızı tanımlayalım:

```csharp
public class BlogPost
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime PublishDate { get; set; }
}

public class Comment
{
    public int Id { get; set; }
    public int BlogPostId { get; set; }
    public string Content { get; set; }
    public DateTime CommentDate { get; set; }
}
```

2. Şimdi, genel bir IRepository arayüzü tanımlayalım:

```csharp
public interface IRepository<T> where T : class
{
    T GetById(int id);
    IEnumerable<T> GetAll();
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
}
```

3. BlogPost ve Comment için özel repository arayüzleri oluşturalım:

```csharp
public interface IBlogPostRepository : IRepository<BlogPost>
{
    IEnumerable<BlogPost> GetRecentPosts(int count);
}

public interface ICommentRepository : IRepository<Comment>
{
    IEnumerable<Comment> GetCommentsByPostId(int postId);
}
```
4. Bu arayüzlerin uygulamalarını yapalım:

```csharp
public class BlogPostRepository : IBlogPostRepository
{
    private readonly AppDbContext _context;

    public BlogPostRepository(AppDbContext context)
    {
        _context = context;
    }

    public BlogPost GetById(int id)
    {
        return _context.BlogPosts.Find(id);
    }

    public IEnumerable<BlogPost> GetAll()
    {
        return _context.BlogPosts.ToList();
    }

    public void Add(BlogPost entity)
    {
        _context.BlogPosts.Add(entity);
        _context.SaveChanges();
    }

    public void Update(BlogPost entity)
    {
        _context.BlogPosts.Update(entity);
        _context.SaveChanges();
    }

    public void Delete(BlogPost entity)
    {
        _context.BlogPosts.Remove(entity);
        _context.SaveChanges();
    }

    public IEnumerable<BlogPost> GetRecentPosts(int count)
    {
        return _context.BlogPosts
            .OrderByDescending(p => p.PublishDate)
            .Take(count)
            .ToList();
    }
}

// CommentRepository için de benzer bir uygulama yapılabilir.
```

5. ASP.NET Core servis yapılandırması:

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
    
    services.AddScoped<IBlogPostRepository, BlogPostRepository>();
    services.AddScoped<ICommentRepository, CommentRepository>();
    // Diğer servis yapılandırmaları...
}
```

6. Controller'da kullanım örneği:

```csharp
[ApiController]
[Route("api/[controller]")]
public class BlogController : ControllerBase
{
    private readonly IBlogPostRepository _blogPostRepository;
    private readonly ICommentRepository _commentRepository;

    public BlogController(IBlogPostRepository blogPostRepository, ICommentRepository commentRepository)
    {
        _blogPostRepository = blogPostRepository;
        _commentRepository = commentRepository;
    }

    [HttpGet]
    public IActionResult GetRecentPosts()
    {
        var recentPosts = _blogPostRepository.GetRecentPosts(5);
        return Ok(recentPosts);
    }

    [HttpGet("{id}/comments")]
    public IActionResult GetPostComments(int id)
    {
        var comments = _commentRepository.GetCommentsByPostId(id);
        return Ok(comments);
    }

    // Diğer action metodları...
}
```
## Nasıl Çalışır?

- Her entity tipi için bir repository arayüzü ve bu arayüzün uygulaması oluşturulur.
- Repository sınıfları, veritabanı işlemlerini gerçekleştirir ve entity'leri yönetir.
- Controller'lar veya servisler, repository'leri kullanarak veri işlemlerini gerçekleştirir.
- Dependency Injection kullanılarak repository'ler uygulamaya enjekte edilir.

# Avantajları

- Veri erişim mantığını merkezileştirir, böylece kod tekrarını azaltır.
- İş mantığı ile veri erişim mantığını ayırır, bu da Single Responsibility prensibine uygundur.
- Test edilebilirliği artırır, çünkü repository'ler kolayca mock'lanabilir.
- Veri kaynağı değiştiğinde (örneğin, SQL Server'dan MongoDB'ye geçiş), sadece repository uygulamalarını değiştirmek yeterlidir.

# Dezavantajları

- Küçük uygulamalar için fazla karmaşık olabilir.
- Her entity için ayrı bir repository oluşturmak, büyük projelerde çok sayıda sınıf oluşturulmasına neden olabilir.
- Yanlış kullanıldığında (örneğin, her metod çağrısında SaveChanges() çağırarak) performans sorunlarına yol açabilir.

# Sonuç
Repository deseni, veri erişimini organize etmek ve uygulamanın geri kalanından ayırmak için etkili bir yöntemdir. Özellikle büyük ve karmaşık uygulamalarda, kodun bakımını ve test edilebilirliğini artırır. Ancak, her proje için uygun olmayabilir ve uygulamanın ihtiyaçlarına göre değerlendirilmelidir.