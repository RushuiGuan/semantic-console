using Albatross.CommandLine;
using Albatross.CommandLine.Annotations;
using Albatross.SemanticConsole.Services;
using System.CommandLine;

namespace Albatross.SemanticConsole.Commands {
	public class ReadTextParams : QuestionParams {
		public const string Verb = "read-text";
		public const string Description = "Prompt for text value";

		[UseOption<Inputs.DefaultOption<string>>]
		public string? Default { get; init; }
	}

	public class ReadText : BaseHandler<ReadTextParams> {
		private readonly ISemanticConsole<Elements.TextQuestion, string> service;

		public ReadText(ISemanticConsole<Elements.TextQuestion, string> service, ParseResult result, ReadTextParams parameters) : base(result, parameters) {
			this.service = service;
		}

		public override async Task<int> InvokeAsync(CancellationToken cancellationToken) {
			var answer = await service.Prompt(new Elements.TextQuestion {
				Context = parameters.Context,
				Question = parameters.Question,
				Default = parameters.Default,
				AllowEmpty = parameters.AllowEmpty,
			}, cancellationToken);
			Writer.WriteLine(answer);
			return 0;
		}
	}
}
