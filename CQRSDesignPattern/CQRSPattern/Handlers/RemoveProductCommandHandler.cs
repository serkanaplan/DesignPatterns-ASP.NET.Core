using CQRSDesignPattern.CQRSPattern.Commands;
using CQRSDesignPattern.DAL;

namespace CQRSDesignPattern.CQRSPattern.Handlers;

public class RemoveProductCommandHandler(Context context)
{
    private readonly Context _context = context;

    public void Handle(RemoveProductCommand command)
    {
        var values = _context.Products.Find(command.Id);
        _context.Products.Remove(values);
        _context.SaveChanges();
    }
}
