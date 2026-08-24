using Albatross.SemanticConsole.Elements;
using Spectre.Console;

namespace Albatross.SemanticConsole.Spectre {
	public class SelectPrompt : ConsolePrompt<Select, string> {
		private readonly ContentWriter contentWriter;

		public SelectPrompt(IAnsiConsole console) : base(console) {
			this.contentWriter = new ContentWriter(console);
		}

		/// <summary>
		/// SelectionPrompt clears its own live display once a choice is submitted, so the pick is written
		/// back afterwards: without it nothing records what was chosen.
		/// </summary>
		public override async Task<string> Prompt(Select element, CancellationToken cancellationToken) {
			WriteContext(element);
			var prompt = new SelectionPrompt<string> {
				Title = element.Question,
				PageSize = element.PageSize,
			};
			prompt.AddChoices(element.Choices);
			if (!string.IsNullOrEmpty(element.Default)) {
				prompt.DefaultValue = element.Default;
			}
			var answer = await console.PromptAsync(prompt, cancellationToken);
			contentWriter.Write(new Content {
				Text = $"{element.Question}: {answer}",
			});
			return answer;
		}
	}
}