# Troubleshooting

**Outcome: the failures this guide actually met, in the order
it met them, each with its reading and its fix.** The
transcripts keep every one verbatim.

## The install script's extraction fails — "No space left on device"

**Seen in:** t00. `dotnet-install.sh` downloads the whole SDK
archive, then `tar` stops part-way claiming the disk is full
— twice, at different files, while a 400 MB test file writes
to the same folder without complaint.

**Reading:** the message names the *symptom of the moment*,
not necessarily the cause; on a shared or unusual filesystem
the script's staging can fail where a direct write succeeds.
**Fix:** do by hand what the script was doing — its own
output names the exact archive URL
(`https://builds.dotnet.microsoft.com/dotnet/Sdk/<version>/dotnet-sdk-<version>-linux-x64.tar.gz`).
Download that file and `tar -xzf` it into the folder you
chose. The lab's install is genuine either way; chapter 3
walks it through.

## NU1301: NuGet cannot load the service index

**Seen in:** t05. Restore of the test project fails after a
long wait: `The HTTP request to 'GET https://api.nuget.org/v3/index.json' has timed out after 100000ms`.

**Reading:** a network report, not a code report — the route
to nuget.org is slow or interrupted, and the per-request
timeout fires first. Check the address answers at all (a
browser, or `curl -I https://api.nuget.org/v3/index.json`),
then **retry the restore**; the lab's retry succeeded in
2.35 minutes and nothing had changed but the hour. Repeated
identical failures on a healthy-looking network are when to
inspect proxy settings and NuGet configuration
(`dotnet nuget list source`).

## `dotnet test` aborts: the testhost never connects

**Seen in:** t06. Both projects build; then
`vstest.console process failed to connect to testhost process`
at 90 seconds — and at 300, with `VSTEST_CONNECTION_TIMEOUT`
raised as the message itself suggests.

**Reading:** the runner and the testhost are separate
processes joined by a loopback socket, and on this sandbox
that socket never connects (the testhost process is alive —
the message says so). Sandboxes and locked-down networks
that interfere with loopback produce exactly this shape.
**On a normal machine** this does not happen; **on this
machine** the workaround is xUnit's own console runner,
which executes the built test assembly in one process
(t07): fetch the `xunit.runner.console` package in your
suite's xUnit version, and run
`dotnet exec .../xunit.console.dll <your>.Tests.dll`. The
tests, the assertions and the verdict are the suite's own;
only the delivery van differs.

## The API listens on the wrong port

**Seen in:** t10. You set `ASPNETCORE_URLS` to your port and
the log announces a different one (the template's, 5155).

**Reading:** `dotnet run` applies `Properties/launchSettings.json`
— a template convenience that sets the address for
development — *over* your environment variables.
**Fix:** `dotnet run --no-launch-profile` when the address
must be yours, or edit `applicationUrl` in the launch
settings when 5155 was meant to be permanent.

## FileNotFoundException, with your own line numbers

**Seen in:** t09. The published program, asked for a CSV
that is not there, ends with
`Unhandled exception. System.IO.FileNotFoundException` and a
stack trace naming `StockFile.Load` and `Program.cs` line 8.

**Reading:** this is .NET's failure format, and it is a gift:
the exception names the cause, and the trace walks *your*
code from the bottom up to the line that asked. An unhandled
exception ends the program (exit code 134 here) — for this
tool, the honest default. If the failure is one you expect,
chapter 6 shows the `try`/`catch` shape; the recipe in
chapter 11 shows turning bad *rows* into a confessed list
instead of a stop.

## FormatException: 'twelve' was not in a correct format

**Seen in:** t09. A quantity written as a word reaches
`int.Parse` in `ParseRow` and the parse throws, naming the
offending string.

**Reading:** the exception tells you the value and the trace
tells you the line — between them they identify the exact
cell of the exact row. It is chapter 6's culture lesson's
other face: parsing is a contract about the file's format,
and a broken contract should name the breaker.

## "You must install .NET" on a machine you published to

**Not met by the lab** (its publish target was itself);
described because chapter 10's bargain makes it the one
failure a published program can still meet. The apphost
cannot find a runtime matching `runtimeconfig.json` — the
target has no .NET, or an older major version. The message
names the version and a download link. Fix by installing
that runtime, or re-publishing `--self-contained` so the
program carries its own (chapter 10 weighs the cost).
