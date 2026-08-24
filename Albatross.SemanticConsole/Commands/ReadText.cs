using Albatross.CommandLine;
using Albatross.CommandLine.Annotations;
using Albatross.SemanticConsole.Services;
using System.CommandLine;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Albatross.SemanticConsole.Commands {
	public class ReadTextParams : QuestionParams {
		public const string Verb = "read-text";
		public const string Description = "Prompt for text value";

		[UseOption<Inputs.DefaultOption<string>>]
		public string? Default { get; init; }

		[Option("regex", Description = "Text validation using regex")]
		public string? RegexValidation { get; init; }
	}

	public class ReadText : BaseHandler<ReadTextParams> {
		private readonly ISemanticConsole<Elements.TextQuestion, string> service;

		public ReadText(ISemanticConsole<Elements.TextQuestion, string> service, ParseResult result, ReadTextParams parameters) : base(result, parameters) {
			this.service = service;
		}

		bool TryValidateDelegate(string input, [NotNullWhen(false)] out string? validationError) {
			validationError = null;
			if (!string.IsNullOrEmpty(parameters.RegexValidation)) {
				var regex = new Regex(parameters.RegexValidation, RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);
				if (!regex.IsMatch(input)) {
					validationError = "invalid input";
					return false;
				}
			}
			return true;
		}

		public override async Task<int> InvokeAsync(CancellationToken cancellationToken) {
			var answer = await service.Prompt(new Elements.TextQuestion {
				Context = parameters.Context,
				Question = parameters.Question,
				Default = parameters.Default,
				AllowEmpty = parameters.AllowEmpty,
				TryValidate = TryValidateDelegate,
			}, cancellationToken);
			Writer.WriteLine(answer);
			return 0;
		}
	}
}
