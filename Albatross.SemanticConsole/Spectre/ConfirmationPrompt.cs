using Albatross.SemanticConsole.Elements;
using Spectre.Console;

namespace Albatross.SemanticConsole.Spectre {
	public class ConfirmationPrompt : ConsolePrompt<Confirmation, bool> {
		public ConfirmationPrompt(IAnsiConsole console) : base(console) {
		}

		public override async Task<bool> Prompt(Confirmation element, CancellationToken cancellationToken) {
			WriteContext(element);
			var prompt = new TextPrompt<string>(element.Question, StringComparer.InvariantCultureIgnoreCase);
			prompt.Choices.AddRange("y", "n");
			prompt.InvalidChoiceMessage = "[red]  ^ invalid input[/]";
			if (element.Default == true) {
				prompt.DefaultValue("y");
			} else if (element.Default == false) {
				prompt.DefaultValue("n");
			}
			var answer = await console.PromptAsync(prompt, cancellationToken);
			return answer == "y";
		}
	}
}