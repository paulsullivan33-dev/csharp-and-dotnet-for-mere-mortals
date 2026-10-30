using StockReport;

namespace StockReport.Tests;

public class StockSummaryTests
{
    // A tiny fixture the tests can hold in their heads: three lines.
    private static readonly List<StockItem> Fixture =
    [
        new("T-001", "Bolts", "Hardware", 100, 2.50m),
        new("T-002", "Work light", "Electrical", 10, 20.00m),
        new("T-003", "Tape", "Electrical", 5, 1.00m),
    ];

    private readonly StockSummary _summary = StockSummary.From(Fixture);

    [Fact]
    public void Line_value_is_quantity_times_price()
    {
        var item = new StockItem("T-009", "Widget", "Tools", 7, 3.30m);
        Assert.Equal(23.10m, item.LineValue);
    }

    [Fact]
    public void Totals_add_up()
    {
        Assert.Equal(3, _summary.LineCount);
        Assert.Equal(115, _summary.TotalUnits);
        Assert.Equal(455.00m, _summary.TotalValue);
    }

    [Fact]
    public void Most_valuable_line_is_the_bolts()
    {
        // 100 x 2.50 = 250.00 beats the work light's 10 x 20.00 = 200.00.
        // (This assertion originally expected the work light — the
        // suite's first run failed on it, and hand arithmetic said
        // the program was right and the test was wrong. See t07.)
        Assert.Equal("T-001", _summary.MostValuableLine.Sku);
        Assert.Equal(250.00m, _summary.MostValuableLine.LineValue);
    }

    [Fact]
    public void Value_by_category_groups_and_orders()
    {
        var byCategory = _summary.ValueByCategory.ToList();
        Assert.Equal(2, byCategory.Count);
        Assert.Equal(("Hardware", 250.00m), byCategory[0]);
        Assert.Equal(("Electrical", 205.00m), byCategory[1]);
    }

    [Fact]
    public void Low_stock_finds_only_the_lines_under_the_threshold()
    {
        var low = _summary.LowStock(10).ToList();
        Assert.Single(low);
        Assert.Equal("T-003", low[0].Sku);
    }

    [Fact]
    public void Csv_rows_parse_into_stock_items()
    {
        StockItem item = StockFile.ParseRow("W-1007,Cordless drill driver,Tools,42,64.50");
        Assert.Equal("W-1007", item.Sku);
        Assert.Equal(42, item.Quantity);
        Assert.Equal(64.50m, item.UnitPrice);
        Assert.Equal(2709.00m, item.LineValue);
    }
}
