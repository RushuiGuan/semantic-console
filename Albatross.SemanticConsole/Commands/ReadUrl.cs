using Albatross.CommandLine;
using Albatross.CommandLine.Annotations;
using Albatross.SemanticConsole.Services;
using System.CommandLine;
using System.Diagnostics.CodeAnalysis;

namespace Albatross.SemanticConsole.Commands {
	public class ReadUrlParams : QuestionParams {
		public const string Verb = "read-url";
		public const string Description = "Ask for a URL and write the answer to stdout";

		[UseOption<Inputs.DefaultOption<string>>]
		public string? Default { get; init; }

		[Option(Description = "The schemes accepted, http and https unless given", AllowMultipleArgumentsPerToken = true)]
		public string[]? Schemes { get; init; }
	}

	public class ReadUrl : BaseHandler<ReadUrlParams> {
		private static readonly string[] defaultSchemes = ["http", "https"];
		private readonly ISemanticConsole<Elements.TextQuestion, string> service;

		public ReadUrl(ISemanticConsole<Elements.TextQuestion, string> service, ParseResult result, ReadUrlParams parameters) : base(result, parameters) {
			this.service = service;
		}

		/// <summary>
		/// A relative URL is refused rather than resolved, because nothing here says what it would be
		/// relative to.
		/// </summary>
		bool TryValidate(string input, [NotNullWhen(false)] out string? validationError) {
			var schemes = parameters.Schemes ?? defaultSchemes;
			if (!Uri.TryCreate(input, UriKind.Absolute, out var uri)) {
				validationError = "^ value is not a URL";
			} else if (!schemes.Contains(uri.Scheme, StringComparer.OrdinalIgnoreCase)) {
				validationError = $"^ scheme '{uri.Scheme}' is not one of {string.Join(", ", schemes)}";
			} else {
				validationError = null;
				return true;
			}
			return false;
		}

		public override async Task<int> InvokeAsync(CancellationToken cancellationToken) {
			var answer = await service.Prompt(new Elements.TextQuestion {
				Context = parameters.Context,
				Question = parameters.Question,
				Default = parameters.Default,
				AllowEmpty = parameters.AllowEmpty,
				TryValidate = TryValidate,
			}, cancellationToken);
			Writer.WriteLine(answer);
			return 0;
		}
	}
}
