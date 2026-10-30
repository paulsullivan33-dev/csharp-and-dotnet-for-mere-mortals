namespace StockReport;

/// <summary>
/// The answers the warehouse asks for, computed from the stock list
/// with LINQ. Kept apart from the console code so tests can check
/// the arithmetic without running the program.
/// </summary>
public class StockSummary
{
    private readonly IReadOnlyList<StockItem> _items;

    private StockSummary(IReadOnlyList<StockItem> items) => _items = items;

    public static StockSummary From(IReadOnlyList<StockItem> items) => new(items);

    public int LineCount => _items.Count;

    public int TotalUnits => _items.Sum(i => i.Quantity);

    public decimal TotalValue => _items.Sum(i => i.LineValue);

    public decimal AverageUnitPrice => _items.Average(i => i.UnitPrice);

    public StockItem MostValuableLine => _items.MaxBy(i => i.LineValue)!;

    public IEnumerable<(string Category, decimal Value)> ValueByCategory =>
        _items.GroupBy(i => i.Category)
              .Select(g => (Category: g.Key, Value: g.Sum(i => i.LineValue)))
              .OrderByDescending(x => x.Value);

    public IEnumerable<StockItem> LowStock(int threshold) =>
        _items.Where(i => i.Quantity < threshold)
              .OrderBy(i => i.Quantity);
}
