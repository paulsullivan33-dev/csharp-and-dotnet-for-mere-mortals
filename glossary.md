# Glossary

The terms this guide uses, in one place. Each is defined
where it first appears, too; this is the quick way back.

**apphost** — The small native executable that starts a .NET
program, so users run `StockReport` instead of
`dotnet StockReport.dll`.

**assembly** — The compiled unit .NET loads and runs: a
`.dll` or `.exe` file containing IL, not machine code.

**async / await** — C#'s way of writing code that waits (for
a network, a disk) without blocking a thread. Named in
chapter 13 as the big topic this guide deliberately leaves
for another day.

**C#** — The language this guide teaches: statically typed,
object-oriented, compiled, garbage-collected. Say "see
sharp".

**class** — A type that holds state and does work. Compare
*record*, a class designed purely for carrying data.

**CLR (Common Language Runtime)** — The part of .NET that
runs compiled programs: JIT-compiles IL, manages memory,
enforces types at run time.

**culture** — A machine's regional conventions for numbers,
dates and money. The lab pins `CultureInfo.InvariantCulture`
when reading and writing files, so formats never drift with
settings.

**decimal** — The 128-bit exact decimal type (`64.50m`).
The money type. Compare `double`, which is for science and
approximations.

**dotnet** — The command-line driver: `new`, `build`, `run`,
`test`, `publish`, and the `--info` questions.

**framework-dependent** — A published program that needs a
.NET runtime already on the target machine. Small; the
default in chapter 10.

**garbage collector** — The runtime service that frees memory
whose objects are no longer reachable, so programs do not
manage lifetimes by hand.

**IL (Intermediate Language)** — What the C# compiler emits:
portable instructions the CLR compiles to machine code at
run time.

**JIT (just-in-time) compilation** — Turning IL into native
machine code as the program runs, method by method.

**lambda** — A small unnamed function written inline —
`i => i.Quantity` reads "given item i, its quantity". The
working parts of LINQ expressions.

**LINQ** — Language Integrated Query: `Where`, `Sum`,
`GroupBy` and friends, for asking questions of collections
(and, with other providers, databases) in the language
itself.

**.NET** — The platform: runtime, standard library and
tooling. Not to be confused with *.NET Framework*, the
older Windows-only platform (chapter 12).

**nullable reference types** — The compiler feature
(`Nullable enable`) that makes `string` mean "never null"
and `string?` mean "possibly null", with warnings where code
ignores the difference.

**NuGet** — The package system: the nuget.org gallery, the
per-project package list in the `.csproj`, and the local
cache restores fill.

**project file (.csproj)** — The XML file naming a project's
target framework, output kind and packages. The recipe
MSBuild cooks from.

**record** — A class designed for data: positional records
get a constructor, properties, value equality and a readable
`ToString()` from one declaration.

**restore** — The step that resolves and downloads a
project's NuGet packages. `dotnet run`, `build`, `test` and
`publish` all do it implicitly when it is out of date.

**runtime** — What executes a program: CLR plus the standard
library, in flavours `Microsoft.NETCore.App` (ordinary) and
`Microsoft.AspNetCore.App` (web).

**SDK** — The developer download: compiler, MSBuild,
templates, the `dotnet` command — everything needed to build,
not just run.

**self-contained** — A published program carrying its own
runtime. Runs on a bare machine; much larger, and must be
published per operating system.

**solution (.slnx)** — A file listing projects that belong
together. Big work uses one; the lab's two projects did not
need it.

**target framework** — The platform version a project is
built for, written as a moniker like `net10.0` in the
`.csproj`.

**top-level statements** — Writing a program as a file of
statements without ceremony classes; the compiler supplies
the entry point.

**xUnit** — The test framework the lab uses: `[Fact]` marks
a test, `Assert.Equal` states what must be true.
