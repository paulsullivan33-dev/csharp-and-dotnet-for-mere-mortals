# Testing Your Code

**Outcome: a test project that checks the program's arithmetic
with real assertions — and an honest account of the two fights
this lab had to have before the green run counted.**
Transcripts **t05** (setup and the failed restore), **t06**
(the `dotnet test` attempts) and **t07** (the run that
counted) are the evidence, kept in full.

## The shape of it

```
$ dotnet new xunit -n StockReport.Tests
$ dotnet add StockReport.Tests reference StockReport
```

A test project is an ordinary project whose packages —
**xUnit** (the test framework), the test SDK, a coverage
collector — come from NuGet, plus a **project reference** to
the code under test. That reference is why chapter 7's
organisation matters: the tests call `StockSummary` and
`StockFile` directly, because those types never touch the
console.

The suite itself (`StockSummaryTests.cs`, kept in
`csharp-lab/`) is six tests over a three-line fixture small
enough to check by hand:

```csharp
[Fact]
public void Totals_add_up()
{
    Assert.Equal(3, _summary.LineCount);
    Assert.Equal(115, _summary.TotalUnits);
    Assert.Equal(455.00m, _summary.TotalValue);
}
```

`[Fact]` marks a test; `Assert.Equal(expected, actual)` is
the claim, and a failed claim fails the test with both values
printed. The six cover line-value arithmetic, the totals, the
most valuable line, grouping by category, the low-stock
filter, and CSV parsing — every behaviour chapter 8's report
depends on.

## Fight one: the restore

Creating the test project restores its packages from
nuget.org — and the lab's first attempt failed:

```
error NU1301: Unable to load the service index for source https://api.nuget.org/v3/index.json.
error NU1301:   The HTTP request to 'GET https://api.nuget.org/v3/index.json' has timed out after 100000ms.
  Failed to restore ... (in 10.11 min).
```

The address answered a browser-style check in two seconds;
the build machine's route to it was simply slow that hour,
and NuGet's per-request timeout gave up first. A plain retry
of `dotnet restore` succeeded in 2.35 minutes. The guide's
rule, learned here rather than invented: **a NU1301 is a
network report, not a code report** — read it as "try again
when the route is healthier", and only start suspecting your
configuration if it fails the same way repeatedly.

## Fight two: the testhost

With packages restored, `dotnet test` built both projects —
and then could not run the tests:

```
vstest.console process failed to connect to testhost process after 90 seconds.
... please set environment variable VSTEST_CONNECTION_TIMEOUT to increase timeout.
Test Run Aborted.
```

How `dotnet test` works explains the error: the console
runner starts a separate **testhost** process and talks to it
over a loopback socket. In this sandbox that connection never
landed — the testhost was alive ("still running", says the
message) but unreachable, at 90 seconds and again at 300. The
same sandbox has bitten other tools that speak over loopback;
it is a property of this build machine, not of the suite, and
a normal machine will never see it. Chapter 14 keeps the
diagnosis steps.

## The run that counted

xUnit ships a second runner of its own,
**xunit.runner.console**, which executes a compiled test
assembly *in its own process* — no socket, no testhost. The
lab fetched the package (version 2.9.3, the suite's own xUnit
version) from nuget.org and pointed it at the very assembly
`dotnet test` had built. Its first run took 0.24 seconds —
and failed one test:

```
Expected: "T-002"
Actual:   "T-001"
```

The test expected the fixture's work light (10 × 20.00 =
200.00) to be the most valuable line. The program answered
the bolts (100 × 2.50 = 250.00). Hand arithmetic sides with
the program: **the test's expectation was mis-added when the
test was written.** The test — not the program — was
corrected, and the re-run is the green this chapter claims:

```
=== TEST EXECUTION SUMMARY ===
   StockReport.Tests  Total: 6, Errors: 0, Failed: 0, Skipped: 0, Time: 0.187s
```

Keep the moral, because it is the real one: a suite that has
never failed has never been seen to work. This one failed
once, at the author's own arithmetic, in front of witnesses —
and that is worth more than a first-try green.

**Check:** on your machine the plain route should simply
work — `dotnet test` from the solution folder, a summary
line, zero failed. If your restore fails instead, you have
met fight one; chapter 14 is written for you.
