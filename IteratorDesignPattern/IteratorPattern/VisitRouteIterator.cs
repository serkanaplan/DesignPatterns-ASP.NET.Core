namespace IteratorDesignPattern.IteratorPattern;

public class VisitRouteIterator(VisitRouteMover visitRouteMover) : IIterator<VisitRoute>
{
    private VisitRouteMover _visitRouteMover = visitRouteMover;
    private int currentIndex = 0;
    public VisitRoute CurrentItem { get; set; }

    public bool NextLocation()
    {
        if (currentIndex < _visitRouteMover.VisitRouteCount)
        {
            CurrentItem = _visitRouteMover.visitRoutes[currentIndex++];
            return true;
        }
        else
            return false;
    }
}
