using MediatorDesignPattern.MediatorPattern.Results;
using MediatR;

namespace MediatorDesignPattern.MediatorPattern.Queries;

public class GetProductUpdateByIDQuery(int id) : IRequest<UpdateProductByIDQueryResult>
{
    public int Id { get; set; } = id;
}
