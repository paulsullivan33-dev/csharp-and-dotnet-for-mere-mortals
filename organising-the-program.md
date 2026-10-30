# Organising the Program — Classes and Records

**Outcome: the lab's program is three small types with one job
each, and you can say why the stock line is a record while the
summary is a class.** Chapter 5's one-liner grew into the real
program by giving each responsibility a home:

| File | Type | Job |
| --- | --- | --- |
| `StockItem.cs` | `record StockItem` | hold one stock line; know its value |
| `StockFile.cs` | `static class StockFile` | turn CSV text into stock lines |
| `StockSummary.cs` | `class StockSummary` | answer questions about the whole list |
| `Program.cs` | top-level statements | take the argument, call the above, print |

## Records: types that are data

```csharp
public record StockItem(
    string Sku,
    string Name,
    string Category,
    int Quantity,
    decimal UnitPrice)
{
    public decimal LineValue => Quantity * UnitPrice;
}
```

A **record** is a class designed for carrying data. That one
declaration — the *positional* form, listing the properties in
the constructor — buys four things a hand-written class would
make you type: a constructor, read-only properties, value
equality (two stock lines with the same values compare equal,
which is what you mean by "the same line"), and a readable
`ToString()`. The body adds one computed property,
`LineValue`, because "what this line is worth" belongs to the
line, not to whatever happens to be adding it up today.

Records suit data that is *read, not revised*: a parsed row, a
configuration, a measurement. The lab never mutates a stock
line after parsing it, and the type says so.

## Classes: types that do work

`StockSummary` is an ordinary **class**, because it holds
state (the list it was built from) and performs work over it:

```csharp
public class StockSummary
{
    private readonly IReadOnlyList<StockItem> _items;

    private StockSummary(IReadOnlyList<StockItem> items) => _items = items;

    public static StockSummary From(IReadOnlyList<StockItem> items) => new(items);

    public int TotalUnits => _items.Sum(i => i.Quantity);
    public decimal TotalValue => _items.Sum(i => i.LineValue);
    // ...
}
```

Three patterns to notice:

- **The field is private and read-only.** Outside code gets
  answers (`summary.TotalValue`), never the list to rummage
  in. What a type hides is most of its design.
- **The constructor is private, with a named factory method**
  (`StockSummary.From(items)`). The call site reads like a
  sentence, and the type keeps the right to change how it is
  built.
- **Answers are properties, not stored fields.** Each total is
  computed from the list when asked. For fourteen rows that is
  obviously fine; the design point is that the summary can
  never disagree with its own data, because it keeps no second
  copy of the truth.

`StockFile` is a **static class** — it has no state at all,
just the two operations `Load` and `ParseRow`, so there is
nothing to construct. Static is the right shape for pure
machinery; it would be the wrong shape for the summary, whose
whole point is the list it holds.

## Why the split matters: chapter 9

This organisation is not tidiness for its own sake. Because
the questions live in `StockSummary` and the parsing in
`StockFile`, the test project can check every total and every
parse **without running the console program at all** — no
screen scraping, no fixture files on disk for most tests. The
console layer, the only part that talks to the terminal, is
the only part the tests leave alone. Programs shaped like
this are the ones that stay testable when they grow.

**Check:** `wc -l` on the four source files shows a program of
about 150 lines, and no file does two jobs. If one of yours
does, that is the seam to cut along.
