namespace Pattern.Observer;

class Customer(string name) : IDeliveryObserver
{
    public void Update(string status) => Console.WriteLine($"{name} notified: {status}");
}
