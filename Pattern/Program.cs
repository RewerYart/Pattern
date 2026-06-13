using Pattern.Core.Facade;
using Pattern.Core.Observer;
using Pattern.Core.Strategy;

// === Задание 1: Facade ===
Console.WriteLine("=== Facade: Home Theater ===");

HomeTheaterFacade theater = new(
    new Projector(),
    new SoundSystem(),
    new StreamingService()
);

theater.StartMovie("Inception");
Console.WriteLine();
theater.StopMovie();

// === Задание 2: Observer ===
Console.WriteLine("\n=== Observer: Delivery Order ===");

DeliveryOrder order = new("Order №4578");

Customer customer1 = new("John");
Customer customer2 = new("Emma");

order.AddObserver(customer1);
order.AddObserver(customer2);

order.Status = "Shipped";

// === Задание 3: Strategy ===
Console.WriteLine("\n=== Strategy: Text Formatter ===");

ITextFormatter upperCase = new UpperCaseFormatter();
ITextFormatter titleCase = new TitleCaseFormatter();

TextEditor editor = new(upperCase);
Console.WriteLine(editor.FormatText("hello world"));

editor.SetFormatter(titleCase);
Console.WriteLine(editor.FormatText("hello world"));
