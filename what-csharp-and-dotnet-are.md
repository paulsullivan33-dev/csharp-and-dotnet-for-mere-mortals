# What C# and .NET Are

You have a job for the computer — totalling a stock file, checking
a folder of photos, serving a small web page on your own network —
and you want a real programming language for it: one with a
compiler that catches mistakes before the program runs, a large
standard library, and the same code working on Windows, Linux and
macOS. **C# is that language, and .NET is the platform it runs
on.**

The two names name different things, and keeping them apart makes
everything else in this guide easier:

- **C#** (say "see sharp") is the language: the words and grammar
  you write. It is a statically typed, object-oriented language
  from Microsoft, first released in 2000, and it has been revised
  steadily ever since — the current version is C# 14, which
  arrived with .NET 10.
- **.NET** is the platform: the runtime that executes your
  compiled program, the standard library it calls into, and the
  tooling (above all the `dotnet` command) that builds, tests and
  packages it. F# and Visual Basic also run on .NET; this guide
  stays with C#.

A C# program is not interpreted line by line, and it is not
compiled straight to machine code either. It takes the middle
road, the same one Java takes: the compiler turns your source
into an **intermediate language (IL)** packaged in an
**assembly** (a `.dll` or `.exe` file), and when the program
runs, the **Common Language Runtime (CLR)** compiles that IL to
native machine code just in time — a **JIT compiler** — and
manages memory for you with a **garbage collector**, so you never
call `free()` and rarely think about who owns an object.

## Why people choose it

- **One SDK, everywhere.** The same `dotnet` command creates,
  builds, tests and publishes projects on Windows, Linux and
  macOS. This guide's lab ran on Linux; nothing in it is
  Linux-specific.
- **The library is the product.** Reading files, parsing JSON,
  talking HTTP, querying data with LINQ, running a web server —
  all ship in the box or in Microsoft's own packages. You write
  less glue.
- **The compiler is a proofreader.** Types are checked before
  the program runs, and modern C# adds *nullable reference
  types*: the compiler warns when a value might be `null` and
  you have not dealt with it. Chapter 6 shows this paying off.
- **It is open source and free.** Since 2016 the modern .NET has
  been developed in the open (github.com/dotnet), under the MIT
  licence, with no per-seat cost. The lab installed it from
  Microsoft's own download server and paid nothing.

## What this guide does with it

The lab builds one small program end to end: a console tool that
reads an invented warehouse stock file (CSV) and prints a report —
totals, value by category, low-stock warnings — with the sums
checked by an independent tool. Around that spine it covers the
whole working loop a beginner actually repeats: install the SDK,
make a project, write and organise code, query data with LINQ,
test the arithmetic with xUnit, and publish a program other
people can run. Chapter 13 (Limits) is honest about what the lab
did not touch: Windows-only .NET Framework, Visual Studio, and
the GUI and mobile frameworks are described, not run.

**Check you can run:** none yet — that is chapter 4's job. If you
finish this chapter able to say which half of the pair is the
language and which is the platform, it has done its work.
