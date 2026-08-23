using Albatross.CommandLine;
using Albatross.CommandLine.Annotations;
using Albatross.SemanticConsole.Services;
using System.CommandLine;

namespace Albatross.SemanticConsole.Commands {
	public class WriteFeedbackParams {
		public const string Verb = "smc write-feedback";
		public const string Description = "Report on the question above, without opening a group of its own";

		[Argument]
		public required string Feedback { get; init; }

		[Option]
		public Enums.Status? Status { get; init; }
	}

	public class WriteFeedback : BaseHandler<WriteFeedbackParams> {
		private readonly ISemanticConsole<Elements.Feedback> service;

		public WriteFeedback(ISemanticConsole<Elements.Feedback> service, ParseResult result, WriteFeedbackParams parameters) : base(result, parameters) {
			this.service = service;
		}

		public override Task<int> InvokeAsync(CancellationToken cancellationToken) {
			service.Write(new Elements.Feedback {
				Status = parameters.Status ?? Enums.Status.Default,
				Text = parameters.Feedback,
			});
			return Task.FromResult(0);
		}
	}
}
