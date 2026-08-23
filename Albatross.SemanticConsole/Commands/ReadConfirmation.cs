using Albatross.CommandLine;
using Albatross.CommandLine.Annotations;
using Albatross.SemanticConsole.Services;
using System.CommandLine;

namespace Albatross.SemanticConsole.Commands {
	public class ReadConfirmationParams {
		public const string Verb = "smc read-confirm";
		public const string Description = "Ask a yes or no question, and write True or False";

		[UseOption<Inputs.ContextOption>]
		public string? Context { get; init; }

		[UseOption<Inputs.QuestionOption>]
		public required string Question { get; init; }

		[UseOption<Inputs.DefaultOption<bool>>]
		public bool Default { get; init; }
	}

	public class ReadConfirmation : BaseHandler<ReadConfirmationParams> {
		private readonly ISemanticConsole<Elements.Confirmation, bool> service;

		public ReadConfirmation(ISemanticConsole<Elements.Confirmation, bool> service, ParseResult result, ReadConfirmationParams parameters) : base(result, parameters) {
			this.service = service;
		}

		public override async Task<int> InvokeAsync(CancellationToken cancellationToken) {
			var answer = await service.Prompt(new Elements.Confirmation {
				Context = parameters.Context,
				Question = parameters.Question,
				Default = parameters.Default,
			}, cancellationToken);
			Writer.WriteLine(answer);
			return 0;
		}
	}
}
