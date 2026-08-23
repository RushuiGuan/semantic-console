using Albatross.SemanticConsole.Elements;
using Albatross.SemanticConsole.Services;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;

namespace Albatross.SemanticConsole.Spectre {
	public static class RegisterExtensions {
		/// <summary>
		/// Registers the Spectre.Console implementation of every element.
		/// </summary>
		/// <remarks>
		/// The two consoles are not interchangeable. What is written goes to stdout, because it is the
		/// transcript the caller asked for; what a prompt puts on the screen goes to stderr, so that
		/// capturing the answer leaves stdout carrying nothing but the answer. The prompt console also has
		/// to declare itself interactive: Spectre calls a console non-interactive as soon as any of the
		/// three streams is redirected, and capturing the answer redirects one.
		/// </remarks>
		public static IServiceCollection AddSpectreConsole(this IServiceCollection services) {
			var output = AnsiConsole.Console;
			var prompt = AnsiConsole.Create(new AnsiConsoleSettings {
				Out = new AnsiConsoleOutput(Console.Error),
				Interactive = InteractionSupport.Yes,
			});
			services.AddSingleton<ISemanticConsole<Elements.Action>>(_ => new ActionWriter(output));
			services.AddSingleton<ISemanticConsole<Info>>(_ => new InfoWriter(output));
			services.AddSingleton<ISemanticConsole<Feedback>>(_ => new FeedbackWriter(output));
			services.AddSingleton<ISemanticConsole<Content>>(_ => new ContentWriter(output));
			services.AddSingleton<ISemanticConsole<TextQuestion, string>>(_ => new TextQuestionPrompt(prompt));
			services.AddSingleton<ISemanticConsole<Secret, string>>(_ => new SecretPrompt(prompt));
			services.AddSingleton<ISemanticConsole<Select, string>>(_ => new SelectPrompt(prompt));
			services.AddSingleton<ISemanticConsole<Confirmation, bool>>(_ => new ConfirmationPrompt(prompt));
			services.AddSingleton<ISemanticConsole<Content, string>>(_ => new ContentPrompt());
			return services;
		}
	}
}