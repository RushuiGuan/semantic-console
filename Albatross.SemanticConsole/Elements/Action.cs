using Status = Albatross.SemanticConsole.Enums.Status;

namespace Albatross.SemanticConsole.Elements {
	public record Action : L1Element {
		public Status Status { get; init; } = Status.Default;
		public bool First { get; init; }
	}
}