using Albatross.CommandLine;
using Albatross.CommandLine.Annotations;
using Albatross.SemanticConsole.Services;
using System.CommandLine;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Albatross.SemanticConsole.Commands {
	public class ReadNumberParams : QuestionParams {
		public const string Verb = "smc read-number";
		public const string Description = "Ask for a number and write the answer to stdout";

		[UseOption<Inputs.DefaultOption<double>>]
		public double? Default { get; init; }

		[Option(Description = "The smallest answer accepted")]
		public double? Min { get; init; }

		[Option(Description = "The largest answer accepted")]
		public double? Max { get; init; }
	}

	public class ReadNumber : BaseHandler<ReadNumberParams> {
		private readonly ISemanticConsole<Elements.TextQuestion, string> service;

		public ReadNumber(ISemanticConsole<Elements.TextQuestion, string> service, ParseResult result, ReadNumberParams parameters) : base(result, parameters) {
			this.service = service;
		}

		bool TryValidate(string input, [NotNullWhen(false)] out string? validationError) {
			if (!double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out var value)) {
				validationError = "^ value is not a number";
			} else if (parameters.Min.HasValue && value < parameters.Min.Value) {
				validationError = $"^ value is below {Format(parameters.Min.Value)}";
			} else if (parameters.Max.HasValue && value > parameters.Max.Value) {
				validationError = $"^ value is above {Format(parameters.Max.Value)}";
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

		static string Format(double value) => value.ToString(CultureInfo.InvariantCulture);
	}
}
