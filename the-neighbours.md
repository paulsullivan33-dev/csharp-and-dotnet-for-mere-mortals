# The Neighbours

**Outcome: you can place C# among the languages around it, and
name the relatives inside its own house.** Characterised from
their documentation and this series' own guides, not
benchmarked here.

## Inside the house: F# and Visual Basic

.NET is a platform with three first-party languages, and a
project can mix them (they compile to the same IL and call
each other freely):

- **F#** is the functional sibling: expression-based, terse,
  beloved for data pipelines and domain modelling where the
  types *are* the specification. If chapter 8's LINQ pipelines
  felt like the natural way to think, F# is that instinct as a
  whole language.
- **Visual Basic** is the elder: BASIC's descendant, kept
  fully supported for its large existing base, with a gentler
  syntax aimed at readability. New projects less often choose
  it, but a great deal of working business code is written in
  it and runs on the same runtime this guide installed.

**PowerShell** is the neighbour this series' readers may
already know: it is itself a .NET program, its objects are
.NET objects, and a PowerShell session can call the same
standard library this guide's C# calls — the automation shell
and the language are two doors into the same house. The
[PowerShell guide](https://paulsullivan33-dev.github.io/powershell-for-mere-mortals/)
in this series works that door.

## The other managed platforms

- **Java** is the closest cousin: a statically typed,
  garbage-collected language compiled to bytecode for its own
  runtime (the JVM), with the same "write once, run anywhere"
  bargain and an ecosystem of the same size and age. C# and
  Java have spent twenty-five years borrowing each other's
  best ideas (generics, lambdas, records, pattern matching all
  crossed in one direction or the other). Choosing between
  them is mostly choosing an ecosystem and an employer, not a
  philosophy. This series has a
  [Java guide](https://paulsullivan33-dev.github.io/java-for-mere-mortals/).
- **Go** makes the opposite runtime bet: no virtual machine,
  no IL — it compiles straight to a native binary with the
  runtime folded in, starts instantly, and keeps the language
  deliberately small. Where .NET brings a large library and a
  large platform, Go brings a small language and a fast,
  boring binary. See the
  [Go guide](https://paulsullivan33-dev.github.io/go-for-mere-mortals/).
- **Rust** removes the garbage collector and replaces it with
  ownership checked at compile time: more power over memory,
  a steeper hill to climb, no runtime to install at all. It
  and C# overlap in systems-adjacent tooling but differ in
  what they make easy. See the
  [Rust guide](https://paulsullivan33-dev.github.io/rust-for-mere-mortals/).

## The scripting shelf

Python, Ruby and Perl solve the small-job end of the world
with less ceremony and no compile step; C#'s answer to "a
quick script" is comparatively heavyweight (a project folder
for a one-off), though top-level statements and `dotnet run`
narrow the gap. The honest summary: reach for C# when the
program will live, grow, be tested, or be handed to someone
else; reach for a scripting language when it will run twice
and be deleted. This guide's stock report began near that
border and crossed it the moment it needed tests.

## The Windows elder: .NET Framework

**NET Framework** (no "Core", no bare ".NET") is the original
Windows-only platform, versions 1.0–4.8.1, still maintaining a
generation of business software. It is a *different* platform
from the one in this guide: Windows-only, updated with
Windows, and in maintenance rather than active development.
Code and skills transfer; projects and libraries often do
not, unchanged. Described here because you will meet the name
constantly — and must not confuse its documentation
(Framework-specific APIs, Windows-only behaviours) with the
modern, cross-platform .NET this guide teaches.
