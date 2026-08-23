namespace Albatross.SemanticConsole.Elements {
	public record Content : ISemanticElement, IPromptElement<string> {
		public string? Context => null;
		public string Question => string.Empty;
		public string? Default => null;
		public bool AllowEmpty => true;
		public string? Text { get; set; }

		public Enums.Level Level { get; init; } = Enums.Level.L1;
	}
}