namespace CompositeDesignPattern.Models;

public class Product(string name) : Component(name)
{
    public override void Display() => Console.WriteLine($"- {Name}");
}
