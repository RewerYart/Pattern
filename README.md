# Pattern - Design Patterns Demo

Учебный проект: реализация паттернов Facade, Observer и Strategy на C# (.NET 8).

## Структура решения

Pattern.sln
├── Pattern.Core/        - библиотека с классами паттернов
├── Pattern/             - консольное приложение (демонстрация)
└── Pattern.WinForms/    - Windows Forms приложение (интерактивный UI)

## Паттерны

| Паттерн   | Классы                                                      |
|-----------|-------------------------------------------------------------|
| Facade    | `HomeTheaterFacade`, `Projector`, `SoundSystem`, `StreamingService` |
| Observer  | `DeliveryOrder`, `IDeliveryObserver`, `Customer`            |
| Strategy  | `TextEditor`, `ITextFormatter`, `UpperCaseFormatter`, `LowerCaseFormatter`, `TitleCaseFormatter` |

## Запуск

### Консольный вывод

```bash
dotnet run --project Pattern/Pattern.csproj

Ожидаемый вывод:

=== Facade: Home Theater ===
Projector is ON
Volume set to 50
Playing movie: Inception

Stopping movie: Inception
Projector is OFF
Sound is muted

=== Observer: Delivery Order ===
John notified: Order №4578 is now Shipped
Emma notified: Order №4578 is now Shipped

=== Strategy: Text Formatter ===
HELLO WORLD
Hello World

Windows Forms

dotnet run --project Pattern.WinForms/Pattern.WinForms.csproj

Откроется окно с тремя вкладками:

- Facade - Home Theater - введи название фильма, нажми Start Movie / Stop Movie
- Observer - Delivery - добавляй покупателей, меняй статус заказа, наблюдай уведомления
- Strategy - Text Formatter - введи текст, выбери стратегию форматирования, нажми Format

Сборка всего решения

dotnet build Pattern.sln
