using CQRSDesignPattern.CQRSPattern.Queries;
using CQRSDesignPattern.CQRSPattern.Results;
using CQRSDesignPattern.DAL;

namespace CQRSDesignPattern.CQRSPattern.Handlers;

public class GetProductUpdateByIDQueryHandler(Context context)
{
    private readonly Context _context = context;

    public GetProductUpdateQueryResult Handle(GetProductUpdateByIDQuery query)
    {
        var values = _context.Products.Find(query.ID);
        return new GetProductUpdateQueryResult
        {
            Description = values.Description,
            Name = values.Name,
            Price = values.Price,
            Stock = values.Stock,
            ProductID = values.ProductID
        };
    }
}
