using MediatorDesignPattern.MediatorPattern.Results;
using MediatR;

namespace MediatorDesignPattern.MediatorPattern.Queries;

public class GetlAllProductQuery : IRequest<List<GelAllProductQueryResult>>
{
}
