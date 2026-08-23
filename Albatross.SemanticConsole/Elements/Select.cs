namespace Albatross.SemanticConsole.Elements {
	public record Select : IPromptElement<string> {
		public required string[] Choices { get; init; }
		public string? Context { get; init; }
		public required string Question { get; init; }
		public string? Default { get; init; }
		public int PageSize { get; init; } = 10;
		public bool AllowEmpty => false;
	}
}