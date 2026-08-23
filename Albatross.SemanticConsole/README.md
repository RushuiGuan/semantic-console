# Albatross.SemanticConsole

Writes console transcripts in terms of what each line *means* — an action, a step, a verdict, a value the caller asked for — instead of the colours and indentation it should be drawn with. A run reads the same whichever script produced it, because the renderer owns the styling and the caller only names the element. Prompts belong to the same vocabulary, so a question, its context and the refusal that follows it sit at the right scope automatically.

Built for installers, deployment scripts and other long-running console programs where an operator has to read the output and answer questions mid-run. Rendering is provided by [Spectre.Console](https://spectreconsole.net/), behind interfaces you can implement yourself.

## Key Features
- **Meaning, Not Formatting** - Elements carry a `Status` and a `Level`; the writer resolves colour and indent, so `Warning` looks the same everywhere it appears
- **Prompts In The Same Vocabulary** - `Context`, `Question` and refusals render at the scope the question opened, so a re-ask does not repeat the explanation
- **Answers Stay Machine-Readable** - Writes go to stdout while prompts render on stderr, so capturing a question's answer leaves stdout carrying nothing but the answer
- **Validated Input** - `TextQuestion` takes a `Sanitize` hook and a `TryValidate` delegate; a rejected answer is refused inline and the question is asked again
- **Renderer-Agnostic Contracts** - `ISemanticConsole<TElement>` writes and `ISemanticConsole<TElement, TResult>` prompts; the Spectre implementation is one `AddSpectreConsole()` call
- **Ready-Made CLI Commands** - Parameter classes and handlers for [Albatross.CommandLine](https://www.nuget.org/packages/Albatross.CommandLine) hosts, each publishing a `Verb` and `Description` constant so the host declares only the verbs it needs

## Quick Start

Register the Spectre implementation, then write elements and ask questions through the injected services.

```csharp
using Albatross.SemanticConsole.Elements;
using Albatross.SemanticConsole.Enums;
using Albatross.SemanticConsole.Services;
using Albatross.SemanticConsole.Spectre;
using Microsoft.Extensions.DependencyInjection;
using Action = Albatross.SemanticConsole.Elements.Action;

var services = new ServiceCollection()
    .AddSpectreConsole()
    .BuildServiceProvider();

var actions = services.GetRequiredService<ISemanticConsole<Action>>();
var steps = services.GetRequiredService<ISemanticConsole<Info>>();
var questions = services.GetRequiredService<ISemanticConsole<TextQuestion, string>>();

// --first suppresses the blank line above: nothing precedes the run
actions.Write(new Action { Text = "Checking prerequisites.", First = true });
steps.Write(new Info { Text = ".NET 10.0 runtime found", Status = Status.Success });
steps.Write(new Info { Text = "The URL Rewrite module is missing. It will be installed.", Status = Status.Warning });

var login = await questions.Prompt(new TextQuestion {
    Context = "Login the server runs as. It needs read and write on mw only.",
    Question = "runtime login:",
}, CancellationToken.None);
```

The two consoles are deliberately different instances. Elements are written to stdout because they are the transcript the caller asked for; prompts are drawn on stderr so that `$login = myapp read-text ...` captures the answer and nothing else.

## Dependencies
- [Spectre.Console](https://www.nuget.org/packages/Spectre.Console) 0.57.2
- [Albatross.CommandLine](https://www.nuget.org/packages/Albatross.CommandLine) 9.0.0-rc.284

## Prerequisites
- .NET 10.0 or later

## Documentation

**[Complete Documentation](https://rushuiguan.github.io/semantic-console/)**
