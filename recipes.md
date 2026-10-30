# Recipes

**Outcome: the stock report bent to the questions a warehouse
asks next.** Each recipe is a small step from code this guide
has already run; the last one was run in full (transcript
**t10**).

## A different file, no rebuild

The program takes its input path as an argument (chapter 8),
so a second warehouse — or next month's file — is free:

```bash
dotnet run -- april-stock.csv        # while developing
./StockReport april-stock.csv        # the published copy
```

Keep the header row and the column order; `StockFile` is
honest about the format it expects (chapter 14 shows what a
broken row does).

## A different "low"

The threshold 30 appears twice: in `Program.cs` (the heading
and the call) and nowhere else. Change both to your number —
10 for a just-in-time shelf, 50 for a nervous one — rebuild,
and the low-stock list is re-cut. To make it an argument
instead, read `args[1]` with `int.Parse` exactly as chapter 6
parses the CSV's own numbers, and pass it to both places.

## "Most valuable" per category

Chapter 8 groups value by category; the same shape answers
"which single line matters most *within* each category":

```csharp
var topByCategory = items
    .GroupBy(i => i.Category)
    .Select(g => g.MaxBy(i => i.LineValue)!)
    .OrderByDescending(i => i!.LineValue);
```

`MaxBy` is the same method `MostValuableLine` uses; grouping
first just asks it five smaller questions instead of one big
one. Add the loop to print them beside chapter 8's table and
the report grows a section without growing a new idea.

## Only count what is really there

A quantity column in the real world contains surprises —
chapter 14's `bad.csv` had the word *twelve*. If a report must
survive dirty input, make `ParseRow` defensive: replace
`int.Parse` with `int.TryParse`, collect the rows that fail,
and print them at the end under "rows I could not read"
rather than stopping the run. Which behaviour is right —
stop loudly or skip and confess — is a decision about the
reader, not the code; this guide's program stops, and the
recipe is the alternative, stated so you can choose it on
purpose.

## Serve it: the same types behind a web API

The lab's stretch goal, run for real (transcript **t10**):
a second project, `dotnet new web`, into which the *same
three files* — `StockItem.cs`, `StockFile.cs`,
`StockSummary.cs` — were copied unchanged. Its whole job is
putting their answers on the wire:

```csharp
app.MapGet("/stock", () =>
{
    StockSummary summary = StockSummary.From(StockFile.Load(csvPath));
    return Results.Json(new
    {
        lines = summary.LineCount,
        totalUnits = summary.TotalUnits,
        totalValue = summary.TotalValue,
        mostValuableLine = summary.MostValuableLine.Sku,
    });
});
```

```
$ curl http://127.0.0.1:18080/stock
{"lines":14,"totalUnits":3279,"totalValue":19021.35,
 "averageUnitPrice":12.35,"mostValuableLine":"W-1007"}
```

Every figure is chapter 8's, because every figure comes from
chapter 8's code — the console program and the web service
are two front doors on one house, which is the whole
argument of chapter 7 made flesh. (`/stock/low` serves the
low-stock list the same way; the API's full `Program.cs` is
kept in `csharp-lab/`.) Two cautions the lab earned: run it
with `--no-launch-profile` when you set the address yourself
— `dotnet run` otherwise applies `launchSettings.json` over
your environment — and a service that re-reads its CSV on
every request is a teaching service, not a production one.
