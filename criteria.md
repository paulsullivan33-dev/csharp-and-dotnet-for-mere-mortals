# Writing criteria for this guide

Every "for Mere Mortals" guide is written to the same nine-point
standard, so a reader who trusts one can trust the rest. Here it
is, with the verification notes for this particular guide below.

1. **Fix it first.** Reader outcomes lead, not action items: each
   chapter opens by naming the outcome it delivers, shows a check
   the reader can run to confirm they got it, and a reasonable
   reader finishes able to state what was done, what it means,
   and what to try next.
2. **Plain language.** Short sentences, everyday vocabulary,
   technical terms defined on first use. No slang, and no
   idioms that only make sense in one country — the reader may
   be translating as they go.
3. **No dead ends.** A guide never tells the reader to do
   something it cannot show them how to verify. Every claim a
   reader relies on — an install, a command, an output — is
   something this guide demonstrates or clearly labels
   documented-only below.
4. **Live-verified where it matters.** Procedures are run for
   real in the guide's lab wherever the build machine allows it,
   and the transcripts kept in the repository are the evidence.
   What could not be run is listed honestly below, never
   presented as if it had been.
5. **General audiences.** Nothing in this guide — examples,
   fixtures, names, addresses — is modelled on any particular
   reader or author's real life, network, or organisations.
   The lab's data is an invented warehouse stock file.
6. **Standalone.** A reader needs this guide, its lab, and
   nothing else. Where another guide would genuinely help, it
   is pointed to once, by name, and the chapter still completes
   without it.
7. **Honest about limits.** Chapter 13 names what the lab did
   not run: Windows and Visual Studio, .NET Framework, the GUI
   and mobile frameworks, real deployment; and what C# is not
   for: bare metal, the two-line throwaway, tiny static
   binaries by default.
8. **The ethics rule is in the text, early.** C# raises no
   interception or impersonation question of the kind other
   guides in this series carry, but the guide draws its own
   lines in chapters 1 and 3: the lab's data is invented, no
   account was created or needed anywhere, and the CLI's
   telemetry opt-out is set and explained before any procedure
   depends on the tool.
9. **Builds clean.** The site builds with MkDocs in strict mode
   with no warnings, and a fresh reader following the chapters
   in order reaches the stated outcome of each.

## Verification notes for this guide

The build machine is a small shared Ubuntu 24.04 sandbox
(limited CPU and RAM, shared with other work; timings are
therefore ranges, and two of this lab's three fights were the
machine's, as the ledger below states plainly). The **.NET SDK
10.0.403** — the current LTS channel — was fetched from
Microsoft's own download server
(`dotnet-sdk-10.0.403-linux-x64.tar.gz`, 240,199,984 bytes,
SHA-256 `cd7224fe8e8ba57c15f1601439ff8785a1a72b54ae5c89dd5302a32dcfbddf18`)
and installed unmodified into the lab's own folder; runtimes
`Microsoft.NETCore.App` and `Microsoft.AspNetCore.App`
10.0.14 came with it. The CLI's telemetry opt-out was set for
every lab command, and the NuGet cache was kept inside the
lab folder. All transcripts live in `csharp-lab/` (t00–t10),
alongside the fixture (`stock.csv`), the program's four
source files, the test suite, the API's `Program.cs`, and the
independent cross-check script (`check.py`).

Run for real, with transcripts as evidence:

- The install itself, twice failed by the official install
  script (extraction dying with `No space left on device`
  while the disk demonstrably had room) and completed by
  unpacking the script's own named archive directly — 621 MB
  SDK folder, failure kept verbatim (t00). First checks:
  `dotnet --version`, the full `--info`, `--list-sdks`,
  `--list-runtimes` (t01).
- `dotnet new console` and the template's first run; then the
  lab's program — a record, a parser and a summary class,
  LINQ throughout — run against the invented fixture (t02,
  t03): 14 lines, **3,279 units**, **total value 19,021.35**,
  average price 12.35, five category values, most valuable
  line W-1007 at 2,709.00, three low-stock lines.
- Every one of those figures reproduced independently by a
  Python cross-check sharing no code with the program, using
  `decimal` arithmetic on both sides (t04).
- An xUnit test project, created and referenced (t05); its
  restore's first failure (NU1301, a proxy timeout to
  nuget.org) kept verbatim, its retry successful (2.35 min).
  `dotnet test` then built both projects but could not
  connect to its testhost in this sandbox at a 90-second or a
  300-second timeout (t06) — so the suite was executed by
  xUnit's own console runner against the same built assembly
  (t07): the first run failed one test, on a test expectation
  the author had mis-added (100 × 2.50 = 250.00, not the
  expected winner's 200.00); the test was corrected, the
  program was not, and the re-run is **6 of 6 green**, exit
  code 0.
- `dotnet publish -c Release`: a 120 KB framework-dependent
  folder, copied to a fresh directory with no project present
  and run there, reproducing the full report (t08).
- Failures, verbatim: missing CSV → `FileNotFoundException`
  with the lab's own line numbers in the trace; a quantity
  written as a word → `FormatException` naming the value;
  both ending the program with exit code 134 (t09).
- The stretch goal, run: an ASP.NET Core minimal API (a
  second project reusing the same three source files
  unchanged) serving the report as JSON on loopback —
  `/stock` returning the same five headline figures,
  `/stock/low` the same three lines, an unknown route
  answering 404. Its one wrinkle is kept: the first launch
  listened on the template's `launchSettings.json` port
  (5155), not the requested one; `--no-launch-profile` put
  it on 18080, where it was proven and then stopped (t10).

Documented only — described from the project's documentation,
not run, and the text does not claim otherwise:

- Everything Windows: Visual Studio, Windows install routes
  beyond naming them, and the Windows-only **.NET
  Framework** platform (characterised in chapter 12).
- The GUI, mobile and browser frameworks: WPF, WinForms,
  MAUI, Blazor. The lab machine has no display and no device;
  the guide teaches the language and workflow they stand on.
- Self-contained and single-file publishing (chapter 10
  weighs them; the lab's target had a runtime, so the
  framework-dependent publish is the one proven).
- Deployment of anything anywhere: Azure, containers,
  servers. Publishing here ends at a folder on this machine.
- NuGet beyond the test stack's own packages and the xUnit
  console runner; F# and Visual Basic beyond their chapter-12
  characterisation; `async`/`await` beyond naming it as the
  next topic a network program will meet.

Fixtures are this guide's own and deliberately generic: a
fourteen-line stock file for a fictional warehouse — invented
SKUs, invented products, prices chosen so the sums can be
checked by hand — and a three-line fixture inside the tests.
No real organisation, person, network or data appears
anywhere in the lab.
