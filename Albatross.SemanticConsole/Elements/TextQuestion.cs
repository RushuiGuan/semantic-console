using System.Diagnostics.CodeAnalysis;

namespace Albatross.SemanticConsole.Elements {
	public record TextQuestion : PromptElement<string> {
		public TryValidateDelegate? TryValidate { get; init; }
		public Func<string?, string?> Sanitize { get; init; } = x => x?.Trim();
		public delegate bool TryValidateDelegate(string input, [NotNullWhen(false)] out string? validationError);
	}
}