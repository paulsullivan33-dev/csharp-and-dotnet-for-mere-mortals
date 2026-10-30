# The Moving Parts

**Outcome: you can name the six pieces a C# program passes
through, and say which of them the `dotnet` command is driving at
each step.** When something breaks later, this map is how you
know which piece to blame.

## The SDK — the workshop

The **.NET SDK** (software development kit) is the one download
that starts everything. It contains the C# compiler, the build
engine (MSBuild), the project templates, the test runner glue,
and the `dotnet` command-line tool itself. The lab's SDK was
version **10.0.403**; `dotnet --info` in chapter 4 shows it
introducing itself. You develop with the SDK. A machine that only
*runs* finished programs does not need it.

## The runtime — the stage

The **.NET runtime** is what executes a compiled program: the
CLR (which JIT-compiles IL to machine code and runs the garbage
collector) plus the standard library assemblies. It ships in
flavours by job: `Microsoft.NETCore.App` for ordinary programs,
`Microsoft.AspNetCore.App` for web programs. The SDK includes
the runtimes, which is why the lab's one install could both
build and run. A finished program can also be published
*self-contained* — carrying its own runtime with it — at the
price of a much larger folder; chapter 10 weighs that choice.

## Your project — the recipe

A **project** is a folder with a **project file** (`.csproj`, an
XML file naming the target framework and settings) and your
`.cs` source files. The lab's project file is eleven lines and
chapter 5 reads them all. Projects that work together — the
program and its tests, in this guide — are grouped loosely by
folder; bigger work uses a *solution* (`.slnx`) file to list
them, which the lab did not need.

## NuGet — the parts warehouse

**NuGet** is .NET's package system: a public gallery
(nuget.org) of libraries, and the machinery that downloads the
ones your project names and keeps them in a local cache. The
test project in chapter 9 is this guide's one real NuGet user —
xUnit and the test SDK are packages, and restoring them is the
step that needs the network. Packages are pinned per project in
the `.csproj`, so two projects can use different versions of the
same library without fighting.

## The compiler and MSBuild — the builders

When you run `dotnet build`, **MSBuild** reads the project file,
works out what is out of date, and calls the C# compiler
(**Roslyn**) on the changed sources. The output lands in
`bin/<Configuration>/<Framework>/` — the lab's program became
`bin/Debug/net10.0/StockReport.dll`, plus a small native
**apphost** executable that starts it. `dotnet run` is build
plus launch in one command; nothing magic, just convenient.

## The shape of a working day

```
dotnet new        make a project from a template
  edit .cs files  the part that is actually programming
dotnet run        build and run while developing
dotnet test       build and run the test projects
dotnet publish    produce the folder you hand to someone else
```

Every command in this guide is one of those five, or a question
(`--info`, `--list-sdks`) asked of the installation itself.
