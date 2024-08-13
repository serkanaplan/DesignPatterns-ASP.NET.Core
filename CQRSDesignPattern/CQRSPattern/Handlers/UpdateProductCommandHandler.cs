using CQRSDesignPattern.CQRSPattern.Commands;
using CQRSDesignPattern.DAL;

namespace CQRSDesignPattern.CQRSPattern.Handlers;

public class UpdateProductCommandHandler(Context context)
{
    private readonly Context _context = context;

    public void Handle(UpdateProductCommand command)
    {
        var values = _context.Products.Find(command.ProductID);
        values.Name = command.Name;
        values.Price = command.Price;
        values.Status = true;
        values.Stock = command.Stock;
        values.Description = command.Description;
        _context.SaveChanges();
    }
}
