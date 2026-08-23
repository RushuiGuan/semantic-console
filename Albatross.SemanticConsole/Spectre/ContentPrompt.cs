using Albatross.SemanticConsole.Elements;
using Albatross.SemanticConsole.Services;
using System.Text;

namespace Albatross.SemanticConsole.Spectre {
	/// <summary>
	/// Content is read from the standard input rather than asked for, so the operator can pipe a list into
	/// the transcript.
	/// </summary>
	public class ContentPrompt : ISemanticConsole<Content, string> {
		public Task<string> Prompt(Content element, CancellationToken cancellationToken) {
			var text = new StringBuilder();
			while (Console.ReadLine() is string line) {
				text.AppendLine(line);
			}
			return Task.FromResult(text.ToString());
		}
	}
}