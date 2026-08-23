using Albatross.CommandLine.Annotations;
using System.CommandLine;

namespace Albatross.SemanticConsole.Inputs {
	/// <summary>
	/// The line explaining the value a question asks for. Every prompt takes one, and it is written above
	/// the question it explains.
	/// </summary>
	[DefaultNameAliases("--context", "-c")]
	public class ContextOption : Option<string> {
		public ContextOption(string name, params string[] aliases) : base(name, aliases) {
			Description = "A line explaining the value, written above the question";
		}
	}
}