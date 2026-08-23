using Albatross.SemanticConsole.Elements;
using Albatross.SemanticConsole.Services;
using Spectre.Console;

namespace Albatross.SemanticConsole.Spectre {
	public class FeedbackWriter : ISemanticConsole<Feedback> {
		private readonly IAnsiConsole console;

		public FeedbackWriter(IAnsiConsole console) {
			this.console = console;
		}

		/// <summary>
		/// Feedback reports on the question above it, so it never opens a group of its own.
		/// </summary>
		public void Write(Feedback element) {
			var style = element.Status.GetStyle(Styles.Info);
			console.Write(new Padder(new Text(element.Text, style), element.Level.ToPadding()));
		}
	}
}