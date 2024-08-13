using MediatorDesignPattern.DAL;
using MediatorDesignPattern.MediatorPattern.Queries;
using MediatorDesignPattern.MediatorPattern.Results;
using MediatR;

namespace MediatorDesignPattern.MediatorPattern.Handlers;

public class GetProductUpdateByIDQueryHandler(Context context) : IRequestHandler<GetProductUpdateByIDQuery, UpdateProductByIDQueryResult>
{
    private readonly Context _context = context;

    public async Task<UpdateProductByIDQueryResult> Handle(GetProductUpdateByIDQuery request, CancellationToken cancellationToken)
    {
        var values = await _context.Products.FindAsync(request.Id);
        return new UpdateProductByIDQueryResult
        {
            ProductID = values.ProductID,
            ProductCategory = values.ProductCategory,
            ProductName = values.ProductName,
            ProductPrice = values.ProductPrice,
            ProductStock = values.ProductStock,
            ProductStockType = values.ProductStockType
        };
    }
}
