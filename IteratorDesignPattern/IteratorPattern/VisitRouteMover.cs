namespace IteratorDesignPattern.IteratorPattern;

public class VisitRouteMover : IMover<VisitRoute>
{
    public List<VisitRoute> visitRoutes = [];

    public int VisitRouteCount { get => visitRoutes.Count; }
    
    public void AddVisitRoute(VisitRoute visitRoute) => visitRoutes.Add(visitRoute);

    public IIterator<VisitRoute> CreateIterator() => new VisitRouteIterator(this);
}
