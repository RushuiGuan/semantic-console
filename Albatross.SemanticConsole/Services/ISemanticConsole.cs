using Albatross.SemanticConsole.Elements;

namespace Albatross.SemanticConsole.Services {
	public interface ISemanticConsole<TElement> where TElement : ISemanticElement {
		void Write(TElement element);
	}
	public interface ISemanticConsole<TElement, TReturnType> where TElement : IPromptElement<TReturnType> {
		Task<TReturnType> Prompt(TElement element, CancellationToken cancellationToken);
	}
}