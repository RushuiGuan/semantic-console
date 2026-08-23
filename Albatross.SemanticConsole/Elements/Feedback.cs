using Status = Albatross.SemanticConsole.Enums.Status;

namespace Albatross.SemanticConsole.Elements {
	// feedback of a question
	public record Feedback : L2Element {
		public Status Status { get; init; } = Status.Default;
	}
}