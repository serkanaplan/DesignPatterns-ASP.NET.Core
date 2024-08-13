namespace TemplateMethodDesignPattern.TemplatePattern;

public class StandardPlan : NetflixPlans
{
    public override string Content(string content) => content;

    public override int CountPerson(int countPerson) => countPerson;

    public override string PlanType(string planType) => planType;

    public override double Price(double price) => price;

    public override string Resolution(string resolution) => resolution;
}
