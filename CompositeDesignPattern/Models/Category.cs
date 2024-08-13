
namespace CompositeDesignPattern.Models;
public class Category(string name) : Component(name)
{
    public readonly List<Component> _components = [];
    public IEnumerable<Component> Components => _components;

    public void Add(Component component) => _components.Add(component);

    public void Remove(Component component) => _components.Remove(component);

    public override void Display()
    {
        Console.WriteLine(Name);

        foreach (var component in _components)
        {
            component.Display();
        }
    }
}
