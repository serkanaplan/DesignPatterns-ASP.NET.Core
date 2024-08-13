
using MediatorDesignPattern.DAL;
using MediatorDesignPattern.MediatorPattern.Commands;
using MediatR;

namespace MediatorDesignPattern.MediatorPattern.Handlers;

public class CreateProductCommandHandler(Context context) : IRequestHandler<CreateProductCommand>
{
    private readonly Context _context = context;

    public async Task Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        _context.Products.Add(new Product
        {
            ProductName = request.ProductName,
            ProductCategory = request.ProductCategory,
            ProductPrice = request.ProductPrice,
            ProductStock = request.ProductStock,
            ProductStockType = request.ProductStockType
        });
        await _context.SaveChangesAsync();
    }
}
