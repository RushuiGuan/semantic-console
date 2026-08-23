using Albatross.SemanticConsole.Elements;
using Spectre.Console;

namespace Albatross.SemanticConsole.Spectre {
	public class TextQuestionPrompt : ConsolePrompt<TextQuestion, string> {
		public TextQuestionPrompt(IAnsiConsole console) : base(console) {
		}

		public override async Task<string> Prompt(TextQuestion element, CancellationToken cancellationToken) {
			WriteContext(element);
			while (true) {
				var prompt = new TextPrompt<string>(element.Question) {
					AllowEmpty = true,
				};
				if (!string.IsNullOrEmpty(element.Default)) {
					prompt.DefaultValue(element.Default);
				}
				var answer = await console.PromptAsync<string>(prompt, cancellationToken);
				answer = element.Sanitize(answer);
				if (string.IsNullOrEmpty(answer)) {
					if (!string.IsNullOrEmpty(element.Default)) {
						return element.Default;
					} else if (element.AllowEmpty) {
						return string.Empty;
					}
				} else {
					if (element.TryValidate != null && !element.TryValidate(answer, out var errMsg)) {
						Refuse(errMsg);
						continue;
					} else {
						return answer;
					}
				}
				Refuse("^ required");
			}
		}
	}
}