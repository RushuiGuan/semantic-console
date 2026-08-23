using Albatross.CommandLine;
using Albatross.CommandLine.Annotations;
using Albatross.SemanticConsole.Services;
using System.CommandLine;

namespace Albatross.SemanticConsole.Commands {
	public class ReadSelectParams : QuestionParams {
		public const string Verb = "smc read-select";
		public const string Description = "Ask the operator to pick one of the choices, and write it to stdout";

		[Option(Description = "The choices to pick from, matched however the operator types them", AllowMultipleArgumentsPerToken = true)]
		public required string[] Choices { get; init; }

		[UseOption<Inputs.DefaultOption<string>>]
		public string? Default { get; init; }
	}

	public class ReadSelect : BaseHandler<ReadSelectParams> {
		private readonly ISemanticConsole<Elements.Select, string> service;

		public ReadSelect(ISemanticConsole<Elements.Select, string> service, ParseResult result, ReadSelectParams parameters) : base(result, parameters) {
			this.service = service;
		}

		public override async Task<int> InvokeAsync(CancellationToken cancellationToken) {
			var answer = await service.Prompt(new Elements.Select {
				Context = parameters.Context,
				Question = parameters.Question,
				Choices = parameters.Choices,
				Default = parameters.Default,
			}, cancellationToken);
			Writer.WriteLine(answer);
			return 0;
		}
	}
}
