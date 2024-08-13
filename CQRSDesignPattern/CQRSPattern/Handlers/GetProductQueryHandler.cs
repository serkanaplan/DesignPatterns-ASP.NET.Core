using CQRSDesignPattern.CQRSPattern.Results;
using CQRSDesignPattern.DAL;

namespace CQRSDesignPattern.CQRSPattern.Handlers;

public class GetProductQueryHandler(Context context)
{
    private readonly Context _context = context;

    public List<GetProductQueryResult> Handle()
    {
        var values = _context.Products.Select(x => new GetProductQueryResult
        {
            ID = x.ProductID,
            Price = x.Price,
            ProductName = x.Name,
            Stock = x.Stock
        }).ToList();
        return values;
    }
}
