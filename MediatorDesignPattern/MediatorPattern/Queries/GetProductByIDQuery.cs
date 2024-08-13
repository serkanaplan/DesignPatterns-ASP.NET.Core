using MediatorDesignPattern.MediatorPattern.Results;
using MediatR;

namespace MediatorDesignPattern.MediatorPattern.Queries;

public class GetProductByIDQuery(int id) : IRequest<GetProductByIDQueryResult>
{
    public int Id { get; set; } = id;
}
