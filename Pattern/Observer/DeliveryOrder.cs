namespace Pattern.Observer;

class DeliveryOrder(string name)
{
    private readonly List<IDeliveryObserver> _observers = [];
    private string _status = string.Empty;

    public string Status
    {
        get => _status;
        set
        {
            _status = value;
            NotifyObservers();
        }
    }

    public void AddObserver(IDeliveryObserver observer) => _observers.Add(observer);
    public void RemoveObserver(IDeliveryObserver observer) => _observers.Remove(observer);

    private void NotifyObservers()
    {
        foreach (var observer in _observers)
            observer.Update($"{name} is now {_status}");
    }
}
