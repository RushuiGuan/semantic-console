using Albatross.CommandLine;
using Albatross.CommandLine.Annotations;
using Albatross.SemanticConsole.Services;
using System.CommandLine;

namespace Albatross.SemanticConsole.Commands {
	public class ReadSecretParams {
		public const string Verb = "smc read-secret";
		public const string Description = "Ask for a value without echoing it, and write the answer to stdout";

		[UseOption<Inputs.ContextOption>]
		public string? Context { get; init; }

		[UseOption<Inputs.QuestionOption>]
		public required string Question { get; init; }
	}

	public class ReadSecret : BaseHandler<ReadSecretParams> {
		private readonly ISemanticConsole<Elements.Secret, string> service;

		public ReadSecret(ISemanticConsole<Elements.Secret, string> service, ParseResult result, ReadSecretParams parameters) : base(result, parameters) {
			this.service = service;
		}

		public override async Task<int> InvokeAsync(CancellationToken cancellationToken) {
			var answer = await service.Prompt(new Elements.Secret {
				Context = parameters.Context,
				Question = parameters.Question,
			}, cancellationToken);
			Writer.WriteLine(answer);
			return 0;
		}
	}
}
