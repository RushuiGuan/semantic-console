using Albatross.SemanticConsole;
using Albatross.SemanticConsole.Elements;
using Albatross.SemanticConsole.Services;
using Spectre.Console;

namespace Albatross.SemanticConsole.Spectre {
	public class ActionWriter : ISemanticConsole<Elements.Action> {
		private readonly IAnsiConsole console;

		public ActionWriter(IAnsiConsole console) {
			this.console = console;
		}

		public void Write(Elements.Action element) {
			if (!element.First) {
				console.WriteLine();
			}
			var style = element.Status.GetStyle(Styles.Header);
			console.Write(new Padder(new Text(element.Text, style), element.Level.ToPadding()));
		}
	}
}