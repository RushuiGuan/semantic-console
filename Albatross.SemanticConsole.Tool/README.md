# Albatross.SemanticConsole.Tool

The `smc` command-line tool, which puts the [Albatross.SemanticConsole](https://www.nuget.org/packages/Albatross.SemanticConsole) vocabulary in reach of shell scripts. A PowerShell or bash installer writes its transcript one verb at a time — `smc write-action`, `smc write-info`, `smc write-feedback` — and asks the operator for values with `smc read-text`, `smc read-select` and friends, getting the same layout, colour and indentation a .NET program would.

Useful when the program driving a run is a script rather than an application, and you still want its output to read like a single coherent transcript.

## Key Features
- **One Verb Per Element** - Twelve verbs cover the whole vocabulary, so a script never writes escape codes or pads lines itself
- **Answers On stdout** - Prompts render on stderr and write only the answer to stdout, so `$name = smc read-text -q "name:"` captures the value cleanly
- **Validated Prompts** - `read-int`, `read-number` and `read-url` refuse a bad answer inline and ask again; `--min` and `--max` bound the range
- **Scriptable Refusals** - `write-feedback --status Error` reports on the question above without opening a group of its own, so a retry loop stays inside the group it started
- **Native AOT** - Published ahead-of-time compiled, so per-line invocation from a loop stays cheap

## Quick Start

### 1. Install

```
dotnet tool install --global Albatross.SemanticConsole.Tool
```

### 2. Write a run

```powershell
smc write-action "Anchor 4.2.1 installer." --first
smc write-info "target C:\Program Files\Anchor"

smc write-action "Checking prerequisites."
smc write-info ".NET 10.0 runtime found" --status Success
smc write-info "The URL Rewrite module is missing. It will be installed." --status Warning

$login = smc read-text `
    --context "Login the server runs as. It needs read and write on mw only." `
    --question "runtime login:"

$port = smc read-int --question "port:" --min 1 --max 65535 --default 443

smc write-action "Installation complete." --status Success
smc write-content "https://localhost:$port"
```

Run `smc --help` for the full verb list, or `smc <verb> --help` for one verb's options.

## Dependencies
- [Albatross.SemanticConsole](https://www.nuget.org/packages/Albatross.SemanticConsole)

## Prerequisites
- .NET 10.0 SDK or later, to install the tool with `dotnet tool install`

## Documentation

**[Complete Documentation](https://rushuiguan.github.io/semantic-console/)**
