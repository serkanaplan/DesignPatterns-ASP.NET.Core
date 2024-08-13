using MediatorDesignPattern.MediatorPattern.Queries ;
using MediatorDesignPattern.MediatorPattern.Results;
using Microsoft.EntityFrameworkCore;
using MediatorDesignPattern.DAL;
using MediatR;

namespace DesignPattern.Meditor.MediatorPattern.Handlers
{
    public class GetAllProductQueryHandler(Context context) : IRequestHandler<GetlAllProductQuery, List<GelAllProductQueryResult>>
    {
        private readonly Context _context = context;

        public async Task<List<GelAllProductQueryResult>> Handle(GetlAllProductQuery request, CancellationToken cancellationToken)
        {
            return await _context.Products.Select(x => new GelAllProductQueryResult
            {
                ProductID = x.ProductID,
                ProductName = x.ProductName,
                ProductCategory = x.ProductCategory,
                ProductPrice = x.ProductPrice,
                ProductStock = x.ProductStock,
                ProductStockType = x.ProductStockType
            }).AsNoTracking().ToListAsync();
        }
    }
}
