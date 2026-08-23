using Albatross.SemanticConsole.Elements;
using Albatross.SemanticConsole.Enums;
using Albatross.SemanticConsole.Services;
using Spectre.Console;
using Status = Albatross.SemanticConsole.Enums.Status;

namespace Albatross.SemanticConsole.Spectre {
	/// <summary>
	/// What every prompt does before it asks: open its group and explain the value. The feedback writer is
	/// built here rather than injected, because a refusal belongs on the same console the question is
	/// asked on.
	/// </summary>
	public abstract class ConsolePrompt<P, T> : ISemanticConsole<P, T> where P : IPromptElement<T> {
		protected readonly IAnsiConsole console;
		private readonly FeedbackWriter feedbackWriter;

		protected ConsolePrompt(IAnsiConsole console) {
			this.console = console;
			this.feedbackWriter = new FeedbackWriter(console);
		}

		protected void WriteContext(P element) {
			console.WriteLine();
			if (!string.IsNullOrEmpty(element.Context)) {
				console.Write(new Padder(new Text(element.Context, Styles.Info), Level.L1.ToPadding()));
			}
		}

		protected void Refuse(string text) {
			feedbackWriter.Write(new Feedback {
				Text = text,
				Status = Status.Error,
			});
		}

		public abstract Task<T> Prompt(P element, CancellationToken cancellationToken);
	}
}