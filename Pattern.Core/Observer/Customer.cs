namespace Pattern.Core.Observer;

public class Customer(string name) : IDeliveryObserver
{
    public string Name { get; } = name;

    public virtual void Update(string status) => Console.WriteLine($"{Name} notified: {status}");
}
