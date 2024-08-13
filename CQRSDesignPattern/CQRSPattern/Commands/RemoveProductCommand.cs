namespace CQRSDesignPattern.CQRSPattern.Commands;

public class RemoveProductCommand(int id)
{
    public int Id { get; set; } = id;
}
