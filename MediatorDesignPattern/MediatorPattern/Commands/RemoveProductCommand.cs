using MediatR;

namespace MediatorDesignPattern.MediatorPattern.Commands;

public class RemoveProductCommand(int id) : IRequest
{
    public int Id { get; set; } = id;
}
