using System.Globalization;
using StockReport;

// StockReport — read a warehouse stock CSV and print a summary.
// Usage: dotnet run -- [path-to-csv]   (default: stock.csv)

string path = args.Length > 0 ? args[0] : "stock.csv";
List<StockItem> items = StockFile.Load(path);
StockSummary summary = StockSummary.From(items);

string Money(decimal value) => value.ToString("0.00", CultureInfo.InvariantCulture);

Console.WriteLine($"Stock report for {path}");
Console.WriteLine($"Lines in stock:    {summary.LineCount}");
Console.WriteLine($"Total units:       {summary.TotalUnits}");
Console.WriteLine($"Total stock value: {Money(summary.TotalValue)}");
Console.WriteLine($"Average unit price: {Money(summary.AverageUnitPrice)}");
Console.WriteLine();
Console.WriteLine("Value by category:");
foreach ((string category, decimal value) in summary.ValueByCategory)
    Console.WriteLine($"  {category,-12} {Money(value),10}");
Console.WriteLine();
StockItem top = summary.MostValuableLine;
Console.WriteLine($"Most valuable line: {top.Sku} {top.Name} — " +
                  $"{top.Quantity} x {Money(top.UnitPrice)} = {Money(top.LineValue)}");
Console.WriteLine();
Console.WriteLine("Low stock (under 30 units):");
foreach (StockItem item in summary.LowStock(30))
    Console.WriteLine($"  {item.Sku} {item.Name} — {item.Quantity} left");
