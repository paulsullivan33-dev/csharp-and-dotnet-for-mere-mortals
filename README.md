# C# & .NET for Mere Mortals

A program begins as a pile of small questions — what is in
stock, what is it worth, what is about to run out — and ends as
a tool that answers them the same way every time. Getting
there in most languages means assembling the toolkit first.
C# takes the opposite bet: one download brings the compiler,
the library and the command that drives them, and the same
afternoon's work runs identically on Windows, Linux and macOS.

This guide takes Microsoft's language — **C#**, pronounced
"see sharp" — and the platform under it, **.NET**, from an
empty machine to a finished, tested, published program,
proving each step in a small lab as it goes. The program is a
stock reporter: it reads an invented warehouse CSV file and
prints totals, values by category, and low-stock warnings,
with every sum checked twice — once by a test suite, once by
a completely different tool.

## What the lab proved

Everything below was run for real; the transcripts live in
[`csharp-lab/`](csharp-lab/) beside this guide.

- The .NET SDK **10.0.403** (the current LTS channel)
  installed into the lab's own folder — by hand, after the
  official install script's extraction failed twice with an
  error the disk did not deserve; the guide keeps the failure
  and the workaround side by side (t00, t01).
- A console program, `StockReport`, built from the standard
  template and grown into three small types — a `record` for
  the stock line, a parser, and a summary class that answers
  questions about the whole file with **LINQ** (t02, t03).
- Its figures — 14 lines, 3,279 units, 19,021.35 of stock
  value — reproduced exactly by an independent Python
  cross-check that shares no code with it (t04).
- A test project in **xUnit** whose restore first failed on a
  network timeout and then succeeded on retry (t05); whose
  `dotnet test` run was defeated by the build machine's
  loopback limits, so the suite was executed by xUnit's own
  console runner instead — where it promptly caught a
  mis-added expectation in one of its own tests, was fixed,
  and ran six of six green (t06, t07).
- The program **published** as a standalone folder and run
  from somewhere else entirely, away from its project (t08);
  its failure modes — a missing file, a malformed row — kept
  verbatim (t09).
- As a stretch goal, the same code served as a small
  **ASP.NET Core** web API on loopback, answering curl with
  the report's figures as JSON (t10).

Described but not run — Windows-only .NET Framework, Visual
Studio, the GUI and mobile frameworks (WPF, WinForms, MAUI,
Blazor), and cloud deployment — and labelled as such wherever
they appear, starting with the Limits chapter.

## The chapters

1. [What C# and .NET Are](what-csharp-and-dotnet-are.md) —
   the language and the platform, and which is which
2. [The Moving Parts](the-moving-parts.md) — SDK, runtime,
   NuGet, the compiler, and the one command that drives them
3. [Installing the .NET SDK](installing-dotnet.md) — four
   routes, and the lab's eventful one
4. [Your First Checks](your-first-checks.md) — proving the
   installation is whole before trusting it
5. [Your First Program](your-first-program.md) — a project
   from the template, read line by line
6. [The Language Core](the-language-core.md) — types, text,
   loops, methods, and null taken seriously
7. [Organising the Program](organising-the-program.md) —
   classes and records, one job each
8. [Working with Data](working-with-data.md) — the stock
   file, LINQ, and a second tool checking the sums
9. [Testing Your Code](testing-your-code.md) — xUnit, a
   failed restore, and what a green run is worth
10. [Publishing Your App](publishing-your-app.md) — the
    folder you hand to someone else
11. [Recipes](recipes.md) — the report bent to new questions
12. [The Neighbours](the-neighbours.md) — F#, PowerShell,
    Java, Go, Rust, and the Windows elder
13. [Limits](limits.md) — what the lab did not run, and what
    C# is not for
14. [Troubleshooting](troubleshooting.md) — the failures this
    guide actually met, in order
15. [Glossary](glossary.md) — the terms, collected

## What you need

A machine that can take a ~240 MB download and a terminal.
No account, no licence fee, no IDE: the SDK is free and open
source, and every step here is a command you can watch.
The lab ran on a small shared Linux machine; timings there
are honest but not promises.

## Part of the series

This guide is one of the [For Mere Mortals](https://paulsullivan33-dev.github.io/mere-mortals/) guides: plain-language guides to the tools programmers and makers use.

Related guides:

- [PowerShell for Mere Mortals](https://paulsullivan33-dev.github.io/powershell-for-mere-mortals/)
- [Java for Mere Mortals](https://paulsullivan33-dev.github.io/java-for-mere-mortals/)
- [Rust for Mere Mortals](https://paulsullivan33-dev.github.io/rust-for-mere-mortals/)
- [Go for Mere Mortals](https://paulsullivan33-dev.github.io/go-for-mere-mortals/)

## License

The writing in this guide is licensed under [CC BY 4.0](https://github.com/paulsullivan33-dev/csharp-and-dotnet-for-mere-mortals/blob/main/LICENSE). Code examples and lab scripts are licensed under the [MIT License](https://github.com/paulsullivan33-dev/csharp-and-dotnet-for-mere-mortals/blob/main/LICENSE-CODE).
