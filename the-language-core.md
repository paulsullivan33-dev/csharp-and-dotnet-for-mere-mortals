# The Language Core

**Outcome: you can read — and write a little of — the C# that
the rest of this guide uses.** This is a working core, not the
whole language: the types, statements and habits that appear in
the lab's program, each shown in code the lab actually compiled.

## Values and types

C# is **statically typed**: every variable has a type, known
when the program is compiled, and the compiler refuses
mismatches a looser language would only meet at run time.

```csharp
int quantity = 42;              // whole numbers
decimal price = 64.50m;         // money: exact decimal, note the m
double ratio = 0.125;           // floating point, for science not money
bool low = quantity < 30;       // true or false
string name = "Cordless drill"; // text
var sku = "W-1007";             // var: the compiler infers string
```

Two choices here are deliberate. **`decimal`, not `double`, for
money**: `double` cannot represent 64.50 exactly, and a stock
report that is out by a cent has failed at its only job. The
`m` suffix marks a decimal literal. And **`var` is not
dynamic**: it asks the compiler to write the type it can
already see from the right-hand side. Nothing is checked any
later for being written `var`.

## Strings that build themselves

**String interpolation** puts values inside text with `$` and
braces — the report's lines are all built this way:

```csharp
Console.WriteLine($"Total units: {summary.TotalUnits}");
Console.WriteLine($"  {category,-12} {value,10}");
```

The `,-12` and `,10` are alignment: pad this value to 12
characters left-aligned, that one to 10 right-aligned. That is
how chapter 8's category table lines up without a formatting
library.

## Making decisions and going round

The grammar will look familiar from any C-family language:
`if` / `else`, `while`, `for`, and the loop this guide leans
on, `foreach` over anything enumerable:

```csharp
foreach (StockItem item in summary.LowStock(30))
    Console.WriteLine($"  {item.Sku} {item.Name} — {item.Quantity} left");
```

A `switch` expression turns a value into another value, handy
for labels:

```csharp
string urgency = item.Quantity switch
{
    < 10 => "order today",
    < 30 => "order this week",
    _   => "plenty",
};
```

## Methods, and saying what you mean

A **method** names a computation. In chapter 7's classes they
are mostly *properties* and LINQ pipelines, but plain methods
carry the parsing:

```csharp
public static StockItem ParseRow(string row)
{
    string[] fields = row.Split(',');
    return new StockItem(
        Sku: fields[0],
        Name: fields[1],
        Category: fields[2],
        Quantity: int.Parse(fields[3], CultureInfo.InvariantCulture),
        UnitPrice: decimal.Parse(fields[4], CultureInfo.InvariantCulture));
}
```

Note the **named arguments** (`Sku:`, `Name:`) at the call to
the record's constructor: with five same-shaped values, naming
which is which is cheap insurance against swapping two — a
mistake the compiler cannot catch when the types agree.

`CultureInfo.InvariantCulture` is the other quiet lesson: text
files do not know your laptop's regional settings. Parsing
`"64.50"` on a machine set to a comma-decimal locale without
naming the culture is how reports quietly go wrong; the lab
parses and formats with the invariant culture throughout, so
its output is the same on any machine.

## Null, taken seriously

With `Nullable` enabled (chapter 5), a `string` promises *not
null*, and a value that may be absent says so: `string?`. The
compiler warns when you use a `string?` as if it were a
`string`. The lab meets this where a lookup might find
nothing:

```csharp
StockItem? found = items.FirstOrDefault(i => i.Sku == wanted);
if (found is null)
    Console.WriteLine("No such line in stock.");
```

`FirstOrDefault` returns `null` on no match, its return type
says `StockItem?`, and the code must deal with the empty case
before touching the result. That is the whole bargain of
modern C#: a paragraph of compiler warnings early, in exchange
for the *null reference exception* — historically the most
common crash in C# programs — being designed out wherever you
let it be.

## Errors are values with a label: exceptions

When something outside the code's control goes wrong — the
file is not there — .NET raises an **exception**, and an
unhandled one ends the program with the exception's name and
message. Chapter 14 shows the lab doing this on purpose:
asking for a CSV that does not exist produces
`System.IO.FileNotFoundException` and the offending path. For
failures you expect, `try` / `catch` turns them into ordinary
decisions; for failures you do not expect, letting the program
stop loudly is a defensible default, and this guide's small
tool takes it.

**Check:** read the three lab source files in `csharp-lab/`
(`StockItem.cs`, `StockFile.cs`, `StockSummary.cs`) and find
one example of each idea above. They are all there, in 90
lines total.
