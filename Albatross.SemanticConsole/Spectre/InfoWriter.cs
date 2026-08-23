using Albatross.SemanticConsole.Elements;
using Albatross.SemanticConsole.Services;
using Spectre.Console;

namespace Albatross.SemanticConsole.Spectre {
	public class InfoWriter : ISemanticConsole<Info> {
		private readonly IAnsiConsole console;

		public InfoWriter(IAnsiConsole console) {
			this.console = console;
		}

		/// <summary>
		/// Info is subordinate to the action above it, so it never opens a group of its own.
		/// </summary>
		public void Write(Info element) {
			var style = element.Status.GetStyle(Styles.Info);
			console.Write(new Padder(new Text(element.Text, style), element.Level.ToPadding()));
		}
	}
}