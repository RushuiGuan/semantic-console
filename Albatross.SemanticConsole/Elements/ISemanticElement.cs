using Albatross.SemanticConsole.Enums;

namespace Albatross.SemanticConsole.Elements {
	public interface IPromptElement<T> {
		string? Context { get; }
		string Question { get; }
		T? Default { get; }
		bool AllowEmpty { get; }
	}


	public interface ISemanticElement {
		Level Level { get; }
	}
	public abstract record L2Element : ISemanticElement {
		public required string Text { get; init; }
		public Level Level => Level.L2;
	}
	public abstract record L1Element : ISemanticElement {
		public required string Text { get; init; }
		public Level Level => Level.L1;
	}
}
