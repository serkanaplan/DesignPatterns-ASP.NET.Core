using FacadeDesignPattern.DAL;

namespace FacadeDesignPattern.FacadePattern;

public class ProductStock
{
    readonly Context context = new();
    public void StockDecrease(int id, int amount)
    {
        var value = context.Products.Find(id);
        value.ProductStock -= amount;
        context.SaveChanges();
    }
}
