using Albatross.CommandLine;
using Albatross.SemanticConsole.Services;
using System.CommandLine;

namespace Albatross.SemanticConsole.Commands {
	public class ReadContentParams {
		public const string Verb = "read-content";
		public const string Description = "Write every line piped in as a content line";
	}

	public class ReadContent : BaseHandler<ReadContentParams> {
		private readonly ISemanticConsole<Elements.Content, string> reader;
		private readonly ISemanticConsole<Elements.Content> writer;

		public ReadContent(ISemanticConsole<Elements.Content, string> reader, ISemanticConsole<Elements.Content> writer, ParseResult result, ReadContentParams parameters) : base(result, parameters) {
			this.reader = reader;
			this.writer = writer;
		}

		public override async Task<int> InvokeAsync(CancellationToken cancellationToken) {
			var element = new Elements.Content();
			element.Text = await reader.Prompt(element, cancellationToken);
			writer.Write(element);
			return 0;
		}
	}
}
