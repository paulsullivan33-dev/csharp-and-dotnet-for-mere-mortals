using StockReport;

// StockApi — the stock report as a tiny web service.
// The same three types as the console program do the work;
// this file only puts their answers on the wire as JSON.
//   GET /            -> a greeting line
//   GET /stock       -> the headline figures
//   GET /stock/low   -> the low-stock lines

string csvPath = Environment.GetEnvironmentVariable("STOCK_CSV") ?? "stock.csv";

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "StockApi: try /stock or /stock/low");

app.MapGet("/stock", () =>
{
    StockSummary summary = StockSummary.From(StockFile.Load(csvPath));
    return Results.Json(new
    {
        lines = summary.LineCount,
        totalUnits = summary.TotalUnits,
        totalValue = summary.TotalValue,
        averageUnitPrice = Math.Round(summary.AverageUnitPrice, 2),
        mostValuableLine = summary.MostValuableLine.Sku,
    });
});

app.MapGet("/stock/low", () =>
{
    StockSummary summary = StockSummary.From(StockFile.Load(csvPath));
    return Results.Json(summary.LowStock(30).Select(i => new
    {
        sku = i.Sku,
        name = i.Name,
        quantity = i.Quantity,
    }));
});

app.Run();
