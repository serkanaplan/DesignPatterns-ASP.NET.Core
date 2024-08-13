# Composite Tasarım Deseni - ASP.NET Core Örneği

## Giriş

Composite tasarım deseni, nesneleri ağaç yapısında düzenlemek için kullanılan yapısal bir desendir. Bu desen, tekil nesneler (leaf) ve nesne kompozisyonları (composite) arasında ayrım yapmadan istemcilerin bu nesneleri kullanmasına olanak tanır.

## Senaryo

Bir şirketin organizasyon yapısını modelleyen bir sistem geliştirdiğimizi düşünelim. Bu sistemde çalışanlar ve departmanlar var. Bir departman diğer departmanları ve çalışanları içerebilir. Composite desenini kullanarak bu hiyerarşik yapıyı oluşturacağız.

## Uygulama

1. Öncelikle, temel `OrganizationComponent` soyut sınıfını tanımlayalım:

```csharp
public abstract class OrganizationComponent
{
    protected string Name;
    protected int Level;

    public OrganizationComponent(string name, int level)
    {
        Name = name;
        Level = level;
    }

    public abstract void Display();
    public virtual void Add(OrganizationComponent component)
    {
        throw new NotImplementedException();
    }
    public virtual void Remove(OrganizationComponent component)
    {
        throw new NotImplementedException();
    }
}
```
2. Şimdi, Employee (Leaf) sınıfını oluşturalım:

````csharp
public class Employee : OrganizationComponent
{
    public Employee(string name, int level) : base(name, level) { }

    public override void Display()
    {
        Console.WriteLine(new string('-', Level) + " " + Name);
    }
}
````

3. Department (Composite) sınıfını oluşturalım:
````csharp
public class Department : OrganizationComponent
{
    private List<OrganizationComponent> _children = new List<OrganizationComponent>();

    public Department(string name, int level) : base(name, level) { }

    public override void Add(OrganizationComponent component)
    {
        _children.Add(component);
    }

    public override void Remove(OrganizationComponent component)
    {
        _children.Remove(component);
    }

    public override void Display()
    {
        Console.WriteLine(new string('-', Level) + "+ " + Name);
        foreach (var component in _children)
        {
            component.Display();
        }
    }
}
````
4. ASP.NET Core Controller'da kullanım örneği:

````csharp
[ApiController]
[Route("api/[controller]")]
public class OrganizationController : ControllerBase
{
    private readonly OrganizationComponent _organization;

    public OrganizationController()
    {
        // Örnek organizasyon yapısı oluşturma
        _organization = new Department("Şirket", 0);
        var sales = new Department("Satış", 1);
        var development = new Department("Geliştirme", 1);
        var marketing = new Department("Pazarlama", 1);

        _organization.Add(sales);
        _organization.Add(development);
        _organization.Add(marketing);

        sales.Add(new Employee("John Doe", 2));
        sales.Add(new Employee("Jane Smith", 2));

        var frontend = new Department("Frontend", 2);
        var backend = new Department("Backend", 2);
        development.Add(frontend);
        development.Add(backend);

        frontend.Add(new Employee("Alice Johnson", 3));
        backend.Add(new Employee("Bob Wilson", 3));

        marketing.Add(new Employee("Charlie Brown", 2));
    }

    [HttpGet]
    public IActionResult GetOrganizationStructure()
    {
        var output = new StringWriter();
        Console.SetOut(output);

        _organization.Display();

        return Ok(output.ToString());
    }
}
````
## Nasıl Çalışır?

- OrganizationComponent soyut sınıfı, hem Employee hem de Department için ortak arayüzü tanımlar.
- Employee sınıfı, hiyerarşinin en alt seviyesindeki bileşenleri (yaprakları) temsil eder.
- Department sınıfı, diğer OrganizationComponent nesnelerini içerebilen kompozit nesneleri temsil eder.
- İstemci (OrganizationController), tekil nesneler ve kompozisyonlar arasında ayrım yapmadan OrganizationComponent arayüzünü kullanır.

# Avantajları

- Hiyerarşik yapıları temsil etmek için doğal bir yol sağlar.
- İstemci kodu, basit ve karmaşık öğeleri aynı şekilde ele alabilir.
- Yeni bileşen türleri eklemek kolaydır ve mevcut kodu değiştirmeyi gerektirmez.

# Dezavantajları

Çok genel bir tasarım oluşturabilir. Belirli sınıflara özgü işlemleri kısıtlamak zor olabilir.
Ağaç yapısında döngüsel bağımlılıkları önlemek için ek kontroller gerekebilir.

# Sonuç
Composite deseni, ağaç benzeri nesne yapılarını temsil etmek ve bu yapılarla çalışmak için güçlü bir araçtır. Bu örnekte gördüğümüz gibi, şirket organizasyon yapısı gibi hiyerarşik yapıları modellemek için idealdir. Desen, istemcilerin tekil nesneler ve nesne kompozisyonlarını tutarlı bir şekilde ele almasına olanak tanır, bu da kodun daha esnek ve genişletilebilir olmasını sağlar.