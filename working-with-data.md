# Working with Data — CSV and LINQ

**Outcome: the lab's program reads a real file and answers
real questions about it — and an independent tool confirms
every number.** Transcripts **t03** (the program) and **t04**
(the cross-check) are the evidence.

## The fixture

The lab's data is invented: `stock.csv`, fourteen lines from a
fictional warehouse — brackets, bolts, work lights, drills,
gloves, paint — each with a SKU, a category, a quantity on the
shelf and a unit price. It sits in `csharp-lab/` beside the
transcripts, small enough to check by eye and big enough for
the interesting questions: five categories, three lines running
low.

## Asking questions with LINQ

**LINQ** (Language Integrated Query) is the standard library's
way of asking questions of collections: filter, transform,
group, total, order — written in the same language as the rest
of the program, checked by the same compiler. Every question
in `StockSummary` is one LINQ expression:

```csharp
public decimal TotalValue => _items.Sum(i => i.LineValue);

public IEnumerable<(string Category, decimal Value)> ValueByCategory =>
    _items.GroupBy(i => i.Category)
          .Select(g => (Category: g.Key, Value: g.Sum(i => i.LineValue)))
          .OrderByDescending(x => x.Value);

public IEnumerable<StockItem> LowStock(int threshold) =>
    _items.Where(i => i.Quantity < threshold)
          .OrderBy(i => i.Quantity);
```

The `i => ...` pieces are **lambda expressions** — small
unnamed functions saying what to do with each item. Read
`LowStock` aloud and it translates itself: *from the items,
where quantity is under the threshold, ordered by quantity.*
The same syntax queries databases and XML with other providers
behind it; learn it on a list and you have learned its shape
everywhere.

Two habits are baked into those three expressions. Nothing is
modified: each returns a new sequence, so asking a question
cannot disturb the data. And `ValueByCategory` returns the
grouping *ordered*, because a report whose rows wander between
runs is a report nobody trusts.

## The run

```
$ dotnet run -- stock.csv
Stock report for stock.csv
Lines in stock:    14
Total units:       3279
Total stock value: 19021.35
Average unit price: 12.35

Value by category:
  Safety          5097.50
  Electrical      4883.25
  Tools           4344.00
  Hardware        3501.00
  Decorating      1195.60

Most valuable line: W-1007 Cordless drill driver — 42 x 64.50 = 2709.00

Low stock (under 30 units):
  W-1009 Spirit level 600mm — 8 left
  W-1006 Insulation tape (roll) — 15 left
  W-1014 Matt emulsion 5L — 26 left
```

(The `--` tells `dotnet run` that `stock.csv` is an argument
for the program, not for the CLI.)

## Proving it with a second tool

A program that adds up is a claim, not a proof. So the lab
checked every figure with something that shares no code with
it: `check.py` (also in `csharp-lab/`), a short Python script
using its own CSV reader and its own arithmetic — `decimal`
there too, so the comparison is exact, not approximate. Its
output (transcript **t04**) agrees on **all of it**: 14 lines,
3,279 units, a total of 19,021.35, the same five category
values in the same order, the same most-valuable line
(2,709.00), the same three low-stock lines in the same order.
When two independent tools agree on a number, the number has
earned its place in the report; when they disagree, you have
found a bug while it is still cheap. This habit — a second
tool, a second pair of eyes — is worth more than any single
test in chapter 9, and the guide uses both.

**Check:** change one quantity in your copy of the CSV, run
both tools, and watch exactly the affected figures move — and
move identically in each.
