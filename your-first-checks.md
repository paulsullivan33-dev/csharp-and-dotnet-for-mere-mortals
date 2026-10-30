# Your First Checks

**Outcome: you can prove the installation is whole — SDK,
runtimes and all — before trusting it with a program.** These
are read-only questions; run them on any machine, including one
someone else set up, when a build misbehaves and you suspect
the tools rather than your code. Transcript **t01** is the
lab's full set of answers.

## `dotnet --version`

The one-line health check:

```
$ dotnet --version
10.0.403
```

That is the SDK version — note it is *not* the same number as
the runtime's. SDK versions are numbered by release train
(10.0.4xx = the fourth SDK feature band for .NET 10); runtime
versions are numbered like ordinary software (10.0.14). Two
different numbers is normal, not a mismatch.

## `dotnet --info`

The full introduction: SDK version and commit, MSBuild
version, operating system and architecture, the runtime
identifier (`linux-x64` for the lab), installed workloads
(none), and — the part worth reading on a strange machine —
the environment variables the CLI has picked up, including
`DOTNET_ROOT`. Half of all "dotnet is broken" reports are an
old SDK earlier on the path or a stray `DOTNET_ROOT`; this
output is where that shows.

## `dotnet --list-sdks` and `--list-runtimes`

```
$ dotnet --list-sdks
10.0.403 [/home/hatch/workspace/csharp-labwork/dotnet/sdk]

$ dotnet --list-runtimes
Microsoft.AspNetCore.App 10.0.14 [...]
Microsoft.NETCore.App 10.0.14 [...]
```

Several SDKs may coexist; each project targets a framework
(`net10.0` in this guide) and the newest suitable SDK builds
it, while the *nearest matching runtime* runs it. The
practical reading: if `--list-runtimes` shows no
`Microsoft.NETCore.App` near your target's version, published
programs will refuse to start with a "you must install .NET"
message — chapter 14 keeps that failure.

**Check:** all three commands answer without errors, and the
SDK and runtime major versions agree (both 10 in the lab). You
are ready to make a program.
