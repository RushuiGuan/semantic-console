namespace Albatross.SemanticConsole.Elements {
	public abstract record PromptElement<T> : IPromptElement<T> {
		public string? Context { get; init; }
		public required string Question { get; init; }
		public T? Default { get; init; }
		public bool AllowEmpty { get; init; }
	}
	public record Confirmation : PromptElement<bool> {
		public Confirmation() {
			Question = "Confirm";
			AllowEmpty = true;
		}
	}
}