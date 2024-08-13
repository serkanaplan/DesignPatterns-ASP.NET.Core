using MediatorDesignPattern.DAL;
using MediatorDesignPattern.MediatorPattern.Commands;
using MediatR;

namespace MediatorDesignPattern.MediatorPattern.Handlers;

public class UpdateProductCommandHandler(Context context) : IRequestHandler<UpdateProductCommand>
{
    private readonly Context _context = context;

    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var values = _context.Products.Find(request.ProductID);
        values.ProductName = request.ProductName;
        values.ProductPrice = request.ProductPrice;
        values.ProductStock = request.ProductStock;
        values.ProductCategory = request.ProductCategory;
        values.ProductStockType = request.ProductStockType;
        await _context.SaveChangesAsync();
    }
}
