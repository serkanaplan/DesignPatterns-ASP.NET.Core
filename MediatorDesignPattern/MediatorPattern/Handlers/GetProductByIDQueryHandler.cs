using MediatorDesignPattern.DAL;
using MediatorDesignPattern.MediatorPattern.Queries;
using MediatorDesignPattern.MediatorPattern.Results;
using MediatR;

namespace MediatorDesignPattern.MediatorPattern.Handlers;

public class GetProductByIDQueryHandler(Context context) : IRequestHandler<GetProductByIDQuery, GetProductByIDQueryResult>
{
    private readonly Context _context = context;

    public async Task<GetProductByIDQueryResult> Handle(GetProductByIDQuery request, CancellationToken cancellationToken)
    {
        var values = await _context.Products.FindAsync(request.Id);
        return new GetProductByIDQueryResult
        {
            ProductID = values.ProductID,
            ProductName = values.ProductName,
            ProductStock = values.ProductStock
        };
    }
}
