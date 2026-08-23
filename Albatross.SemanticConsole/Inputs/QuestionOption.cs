using Albatross.CommandLine.Annotations;
using System.CommandLine;

namespace Albatross.SemanticConsole.Inputs {
	[DefaultNameAliases("--question", "-q")]
	public class QuestionOption : Option<string> {
		public QuestionOption(string name, params string[] aliases) : base(name, aliases) {
			Description = "The question put to the operator";
		}
	}
}