namespace CompositeDesignPattern.Models;

public abstract class Component(string name)
{
    public int Id { get; set; }
    public string Name { get; set; } = name;

    public abstract void Display();
}
