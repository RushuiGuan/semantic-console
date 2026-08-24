using Albatross.CommandLine;
using Albatross.CommandLine.Annotations;
using Albatross.SemanticConsole.Services;
using System.CommandLine;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Albatross.SemanticConsole.Commands {
	public class ReadIntParams : QuestionParams {
		public const string Verb = "read-int";
		public const string Description = "Prompt for an integer";

		[UseOption<Inputs.DefaultOption<int>>]
		public int? Default { get; init; }

		[Option(Description = "The smallest accepted value")]
		public int? Min { get; init; }

		[Option(Description = "The largest accepted value")]
		public int? Max { get; init; }
	}

	public class ReadInt : BaseHandler<ReadIntParams> {
		private readonly ISemanticConsole<Elements.TextQuestion, string> service;

		public ReadInt(ISemanticConsole<Elements.TextQuestion, string> service, ParseResult result, ReadIntParams parameters) : base(result, parameters) {
			this.service = service;
		}

		bool TryValidate(string input, [NotNullWhen(false)] out string? validationError) {
			if (!int.TryParse(input, NumberStyles.Integer, CultureInfo.InvariantCulture, out var value)) {
				validationError = "invalid integer";
			} else if (parameters.Min.HasValue && value < parameters.Min.Value) {
				validationError = $"value is less than {Format(parameters.Min.Value)}";
			} else if (parameters.Max.HasValue && value > parameters.Max.Value) {
				validationError = $"value is greater than {Format(parameters.Max.Value)}";
			} else {
				validationError = null;
				return true;
			}
			return false;
		}

		public override async Task<int> InvokeAsync(CancellationToken cancellationToken) {
			var answer = await service.Prompt(new Elements.TextQuestion {
				Context = parameters.Context,
				Question = parameters.Question,
				Default = parameters.Default.HasValue ? Format(parameters.Default.Value) : null,
				AllowEmpty = parameters.AllowEmpty,
				TryValidate = TryValidate,
			}, cancellationToken);
			Writer.WriteLine(answer);
			return 0;
		}

		static string Format(int value) => value.ToString(CultureInfo.InvariantCulture);
	}
}
