using CQRSDesignPattern.CQRSPattern.Queries;
using CQRSDesignPattern.CQRSPattern.Results;
using CQRSDesignPattern.DAL;

namespace CQRSDesignPattern.CQRSPattern.Handlers;

public class GetProductByIDQueryHandler(Context context)
{
    private readonly Context _context = context;

    public GetProductByIDQueryResult Handle(GetProductByIDQuery query)
    {
        var values = _context.Set<Product>().Find(query.Id);
        return new GetProductByIDQueryResult
        {
            Name = values.Name,
            Price = values.Price,
            ProductID = values.ProductID,
            Stock = values.Stock
        };
    }
}
