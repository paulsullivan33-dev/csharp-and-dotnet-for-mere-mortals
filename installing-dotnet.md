# Installing the .NET SDK

**Outcome: a `dotnet` command on your machine, installed by a
route you chose on purpose.** There are four honest routes; they
differ in who keeps the SDK updated and where the files live.

## The routes

- **Your system package manager** (the Microsoft package
  repositories for Ubuntu/Debian/Fedora, `brew` on macOS,
  `winget` on Windows). Easiest to keep updated; the version
  offered can lag a release behind Microsoft's own.
- **The official install script** (`dotnet-install.sh` on Linux
  and macOS, `dotnet-install.ps1` on Windows, both from
  https://dot.net/v1). Downloads the current SDK into a folder
  *you* pick — no administrator rights, no system changes, and
  several SDK versions can live side by side. This is the route
  the lab took.
- **The standalone tarball/zip** the script itself downloads,
  unpacked by hand. The fallback when the script misbehaves —
  which, in this lab, it did; see below.
- **Visual Studio** (Windows, and formerly macOS) bundles an SDK
  with the IDE. Convenient if you already live there; this guide
  does everything from the command line, where the steps are
  visible and identical on every operating system.

## The lab's install, honestly

The lab used the install script, aimed at a folder inside its
own workspace so nothing touched the system:

```bash
curl -sSL https://dot.net/v1/dotnet-install.sh -o dotnet-install.sh
./dotnet-install.sh --channel LTS \
    --install-dir ~/workspace/csharp-labwork/dotnet
```

The script downloaded the genuine SDK — **.NET 10.0.403**, the
current LTS channel — a 240,199,984-byte archive, and then its
extraction failed, twice, with `tar` reporting *No space left on
device* part-way through (transcript **t00** keeps the exact
lines). The disk was not full: a 400 MB test file wrote happily
to the same folder before and after. Downloading the *same
archive the script names* and unpacking it with `tar` directly
into the same folder worked first try, giving a 621 MB `dotnet`
folder. The lesson the guide keeps is not "the script is bad" —
it is used by build servers everywhere — but that an install
script is a convenience, not a mystery: when it fails, its own
output tells you the exact file it wanted, and fetching that
file yourself is a legitimate route, not a hack.

Two settings make a script-installed SDK usable and quiet. Put
its folder on your path, and tell the CLI to keep its state in
known places:

```bash
export DOTNET_ROOT="$HOME/workspace/csharp-labwork/dotnet"
export PATH="$DOTNET_ROOT:$PATH"
export DOTNET_CLI_TELEMETRY_OPTOUT=1   # no usage phone-home
export NUGET_PACKAGES="$HOME/workspace/csharp-labwork/nuget-packages"
```

`DOTNET_CLI_TELEMETRY_OPTOUT=1` deserves its sentence: the CLI
sends anonymous usage counts to Microsoft by default; the
opt-out is documented, respected, and set for every command in
this guide's lab.

**Check:** `dotnet --version` prints a version — the lab's
answered `10.0.403`. Chapter 4 asks the installation the longer
questions.
