using ChainOfResponsibiltyDesignPattern.Models;

namespace ChainOfResponsibiltyDesignPattern.ChainOfResponsibility;

public abstract class Employee
{
    protected Employee NextApprover;
    public void SetNextApprover(Employee superVisor) => NextApprover = superVisor;
    public abstract void ProcessRequest(CustomerProcessViewModel req);
}
