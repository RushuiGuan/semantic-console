using Albatross.SemanticConsole.Enums;

namespace Albatross.SemanticConsole.Elements {
	public record Content : ISemanticElement, IPromptElement<string> {
		public string? Context => null;
		public string Question => string.Empty;
		public string? Default => null;
		public bool AllowEmpty => true;
		public Level Level => Level.L2;
		public string Text { get; set; } = string.Empty;
	}
}