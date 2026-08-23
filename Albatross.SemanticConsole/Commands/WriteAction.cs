using Albatross.CommandLine;
using Albatross.CommandLine.Annotations;
using Albatross.SemanticConsole.Services;
using System.CommandLine;

namespace Albatross.SemanticConsole.Commands {
	public class WriteActionParams {
		public const string Verb = "smc write-action";
		public const string Description = "";

		[Argument]
		public required string Action { get; init; }

		[Option]
		public Enums.Status? Status { get; init; }

		[Option]
		public bool First { get; init; }
	}

	public class WriteAction : BaseHandler<WriteActionParams> {
		private readonly ISemanticConsole<Elements.Action> service;

		public WriteAction(ISemanticConsole<Elements.Action> service, ParseResult result, WriteActionParams parameters) : base(result, parameters) {
			this.service = service;
		}

		public override Task<int> InvokeAsync(CancellationToken cancellationToken) {
			service.Write(new Elements.Action {
				First = parameters.First,
				Status = parameters.Status ?? Enums.Status.Default,
				Text = parameters.Action,
			});
			return Task.FromResult(0);
		}
	}
}
