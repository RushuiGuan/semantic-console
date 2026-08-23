using Albatross.SemanticConsole.Enums;

namespace Albatross.SemanticConsole.Elements {
	public record Secret : IPromptElement<string> {
		public string? Context { get; init; }
		public required string Question { get; init; }
		public string? Default => null;
		public bool AllowEmpty => false;
		public Level Level => Level.L1;
	}
}