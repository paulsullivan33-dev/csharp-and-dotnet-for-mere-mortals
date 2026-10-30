# Limits

**Outcome: you know where the floor of this guide is — what
was not run, what C# is not for, and which of its famous
features this lab never touched.**

## What the lab did not run

Stated plainly, as this series always states it:

- **Windows anything.** The lab is Linux. .NET on Windows —
  including Visual Studio, the big IDE most C# developers use
  — is documented-only here, as is the Windows-only **.NET
  Framework** (chapter 12).
- **The graphical and mobile frameworks** — WPF, WinForms,
  MAUI, Blazor. All real, all substantial, all needing a
  display or a device this build machine does not have. The
  guide teaches the language and the command-line workflow
  they all stand on.
- **A web service, beyond the stretch goal that ran.** The
  lab's minimal ASP.NET Core API ran on loopback and answered
  curl with the report's own figures (t10, and chapter 11
  shows the code). Everything past that — hosting it for
  real, TLS, authentication, keeping it up — is
  documented-only.
- **Azure, or any deployment target.** Publishing (chapter 10)
  produces a folder on this machine. Where that folder goes
  next — a server, a container, a cloud — is operations, and
  it is documented-only.
- **Packages beyond the test stack.** NuGet was exercised for
  xUnit and its companions only. The guide does not review
  third-party libraries, and chapter 9's restore is also the
  lab's whole experience of the gallery's speed on a slow
  network: it worked, and it was not quick.

## What C# is not for

- **Bare metal and kernels.** The garbage collector and the
  runtime are the bargain; work that must account for every
  byte and cycle belongs to C, Rust or Zig. (.NET has
  embedded and ahead-of-time corners — NativeAOT exists — but
  they are specialisms, not the mainstream this guide
  teaches.)
- **The two-line throwaway.** A project, a build and a publish
  folder is real overhead for a script you will run once.
  Chapter 12 names the honest border.
- **Instant start, tiny footprint, by default.** A
  framework-dependent console app starts fast enough for
  tools and reports; a self-contained one carries a runtime.
  Neither is a single static 2 MB binary of the kind Go
  produces without trying, and pretending otherwise helps no
  one choosing a tool.

## Traps inside the language itself

- **`double` for money** (chapter 6) — exactness is a choice
  you make per variable.
- **Culture, quietly** — parsing and formatting follow the
  machine's locale unless told otherwise; the lab pins the
  invariant culture for file formats and says so every time.
- **The old documentation.** Thirty years of .NET-shaped
  history means search results mix .NET Framework, .NET Core
  and modern .NET advice freely. Check which platform a page
  is about before following it; the version line in chapter
  4's `dotnet --info` is how you know what *you* have.
- **Async is a colour.** Code that awaits is written
  differently from code that does not, and the boundary
  spreads. The lab's program is small enough to stay
  synchronous throughout, which this guide counts as a
  feature; a network-heavy program will meet `async`/`await`
  on its first day, and it deserves a guide of its own.
