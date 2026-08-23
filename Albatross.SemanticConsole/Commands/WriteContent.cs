using Albatross.CommandLine;
using Albatross.CommandLine.Annotations;
using Albatross.SemanticConsole.Services;
using System.CommandLine;

namespace Albatross.SemanticConsole.Commands {
	public class WriteContentParams {
		public const string Verb = "smc write-content";
		public const string Description = "Write a value the operator picks from, types or copies";

		[Argument]
		public required string Content { get; init; }
	}

	public class WriteContent : BaseHandler<WriteContentParams> {
		private readonly ISemanticConsole<Elements.Content> service;

		public WriteContent(ISemanticConsole<Elements.Content> service, ParseResult result, WriteContentParams parameters) : base(result, parameters) {
			this.service = service;
		}

		public override Task<int> InvokeAsync(CancellationToken cancellationToken) {
			service.Write(new Elements.Content {
				Text = parameters.Content,
			});
			return Task.FromResult(0);
		}
	}
}
