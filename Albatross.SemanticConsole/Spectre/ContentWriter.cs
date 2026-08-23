using Albatross.SemanticConsole.Elements;
using Albatross.SemanticConsole.Services;
using Spectre.Console;

namespace Albatross.SemanticConsole.Spectre {
	public class ContentWriter : ISemanticConsole<Content> {
		private readonly IAnsiConsole console;

		public ContentWriter(IAnsiConsole console) {
			this.console = console;
		}

		public void Write(Content element) {
			if (!string.IsNullOrEmpty(element.Text)) {
				console.Write(new Padder(new Text(element.Text, Styles.Content), element.Level.ToPadding()));
			}
		}
	}
}