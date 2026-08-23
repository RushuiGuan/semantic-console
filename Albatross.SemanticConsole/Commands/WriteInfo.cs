using Albatross.CommandLine;
using Albatross.CommandLine.Annotations;
using Albatross.SemanticConsole.Services;
using System.CommandLine;

namespace Albatross.SemanticConsole.Commands {
	public class WriteInfoParams {
		public const string Verb = "write-info";
		public const string Description = "Write a line at step scope, subordinate to the action above it";

		[Argument]
		public required string Info { get; init; }

		[Option]
		public Enums.Status? Status { get; init; }
	}

	public class WriteInfo : BaseHandler<WriteInfoParams> {
		private readonly ISemanticConsole<Elements.Info> service;

		public WriteInfo(ISemanticConsole<Elements.Info> service, ParseResult result, WriteInfoParams parameters) : base(result, parameters) {
			this.service = service;
		}

		public override Task<int> InvokeAsync(CancellationToken cancellationToken) {
			service.Write(new Elements.Info {
				Status = parameters.Status ?? Enums.Status.Default,
				Text = parameters.Info,
			});
			return Task.FromResult(0);
		}
	}
}
