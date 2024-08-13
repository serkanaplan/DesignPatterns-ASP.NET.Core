namespace IteratorDesignPattern.IteratorPattern;

public interface IMover<T>
{
    IIterator<T> CreateIterator();
}
