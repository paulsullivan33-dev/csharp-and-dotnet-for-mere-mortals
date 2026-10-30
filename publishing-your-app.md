# Publishing Your App

**Outcome: a folder that *is* the program — copied somewhere
else, run there, no project in sight.** Transcript **t08**.

## Publish

```
$ dotnet publish StockReport -c Release -o publish-out
  StockReport -> .../bin/Release/net10.0/StockReport.dll
  StockReport -> .../publish-out/
```

`dotnet publish` builds in the **Release** configuration
(optimised, no debug padding) and gathers everything the
program needs into one folder:

```
StockReport                  the apphost: run this
StockReport.dll              your program, as IL
StockReport.deps.json        what it depends on
StockReport.runtimeconfig.json   which runtime it wants
StockReport.pdb              debugging symbols
```

The lab's whole folder is **120 KB**. That smallness is the
**framework-dependent** bargain: the folder assumes the
target machine already has a .NET 10 runtime, and in exchange
it is tiny, and the same folder runs on any operating system
that has one — the apphost is the only native file, and even
it has a per-OS twin when you publish for another RID.

## Running it somewhere else

The lab copied `publish-out/` and the CSV to a fresh directory
— no `.csproj`, no source, nothing of the project — and ran
it:

```
$ cd elsewhere && ./StockReport stock.csv
Stock report for stock.csv
Lines in stock:    14
... (the full report, identical to chapter 8's)
```

It works because everything the program needs is either in
the folder or in the shared runtime it names. This is the
moment a program stops being a project and becomes a tool:
the folder can be zipped, copied to a server, or handed to a
colleague, and chapter 14 covers the one failure it can
still meet — a machine with no suitable runtime answers
with a "You must install .NET" message naming the version
it wants.

## The other bargain: self-contained

Publishing with `--self-contained -r linux-x64` packs the
runtime *into* the folder: no .NET needed on the target at
all. The costs are real — the folder grows from kilobytes to
tens of megabytes, the output is per-operating-system (a
`linux-x64` folder will not run on Windows), and *you* become
responsible for shipping runtime security updates with your
program. The lab did not need it (its target had the runtime
— it was the same machine) and so did not run it; the choice
rule is short: **framework-dependent for machines you
control, self-contained for machines you cannot touch.**
A third option, single-file publishing
(`-p:PublishSingleFile=true`), folds the folder into one
executable-looking file and is usually combined with
self-contained.

**Check:** publish, copy the folder somewhere new, and run
the report there against the fixture. Identical numbers,
no project — that is publishing, proved.
