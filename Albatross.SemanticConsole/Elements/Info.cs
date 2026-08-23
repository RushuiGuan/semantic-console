using Status = Albatross.SemanticConsole.Enums.Status;

namespace Albatross.SemanticConsole.Elements {
	public record Info : L2Element {
		public Status Status { get; init; } = Status.Default;
	}
}