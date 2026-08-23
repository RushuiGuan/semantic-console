using Albatross.CommandLine.Annotations;

namespace Albatross.SemanticConsole.Commands {
	public class QuestionParams {
		[UseOption<Inputs.ContextOption>]
		public string? Context { get; init; }

		[UseOption<Inputs.QuestionOption>]
		public required string Question { get; init; }

		[UseOption<Inputs.AllowEmptyOption>]
		public bool AllowEmpty { get; init; }
	}
}
