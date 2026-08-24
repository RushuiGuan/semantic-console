using Albatross.SemanticConsole.Elements;
using Spectre.Console;

namespace Albatross.SemanticConsole.Spectre {
	public class SecretPrompt : ConsolePrompt<Secret, string> {
		public SecretPrompt(IAnsiConsole console) : base(console) {
		}

		public override async Task<string> Prompt(Secret element, CancellationToken cancellationToken) {
			WriteContext(element);
			while (true) {
				var prompt = new TextPrompt<string>(element.Question).Secret();
				var answer = await console.PromptAsync(prompt, cancellationToken);
				if (answer.Length > 0) {
					return answer;
				}
				Refuse("required");
			}
		}
	}
}