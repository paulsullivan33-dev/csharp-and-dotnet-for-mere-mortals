# Your First Program

**Outcome: a project you created, whose files you have read,
printing its first line.** Transcript **t02** is the whole
sequence.

## Making the project

```
$ dotnet new console -n StockReport
The template "Console App" was created successfully.
...
  Restored .../StockReport.csproj (in 181 ms).
```

`dotnet new` copies a template and immediately *restores* it —
resolving any packages it names (a plain console project names
none, so this is quick and needs no network). Templates exist
for the common shapes: `console`, `classlib`, `xunit`, `web`,
`webapi`, `worker`, and more (`dotnet new list`). Everything
the template writes is yours to edit; nothing is generated
behind your back at build time except the `obj/` folder of
intermediate files.

You get two files that matter:

**`Program.cs`** — the whole template program:

```csharp
Console.WriteLine("Hello, World!");
```

One line, no ceremony. Modern C# allows **top-level
statements**: a file of statements *is* the program, and the
compiler wraps them in the entry-point machinery for you.
Files can still declare classes and records below the
statements, and chapter 7's program does exactly that split —
statements in `Program.cs`, types in their own files.

**`StockReport.csproj`** — the project file, worth reading
once in full:

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net10.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>
</Project>
```

- `OutputType Exe` — this project builds a program, not a
  library.
- `TargetFramework net10.0` — it runs on .NET 10. This one
  line is also the compatibility contract: a machine needs a
  .NET 10 (or later, compatible) runtime to run it.
- `ImplicitUsings enable` — the everyday namespaces (`System`,
  `System.IO`, `System.Linq`, …) are imported for you; that is
  why the one-line program needs no `using` lines.
- `Nullable enable` — nullable reference types are on: the
  compiler will warn where a value could be `null` and the
  code has not reckoned with it. Chapter 6 puts this to work.

## Running it

```
$ cd StockReport && dotnet run
Hello, World!
```

`dotnet run` restores if needed, builds into
`bin/Debug/net10.0/`, and launches the result. The `Debug`
configuration is the default: slower, with debugging symbols.
The `Release` configuration (`-c Release`) is the optimised
one chapter 10 publishes. Build on its own is `dotnet build`;
the compiled program can also be run directly —
`./bin/Debug/net10.0/StockReport` (the apphost) or
`dotnet bin/Debug/net10.0/StockReport.dll` — which matters in
chapter 10, when the program leaves the project folder.

**Check:** your run prints `Hello, World!` and
`bin/Debug/net10.0/` contains a `.dll` with your project's
name. The machinery works; the next chapters make it earn
its keep.
