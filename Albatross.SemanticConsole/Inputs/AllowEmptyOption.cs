using Albatross.CommandLine.Annotations;
using System.CommandLine;

namespace Albatross.SemanticConsole.Inputs {
	/// <summary>
	/// Whether a question takes an empty answer. Without it the question is asked again until the operator
	/// gives a value.
	/// </summary>
	[DefaultNameAliases("--allow-empty", "-e")]
	public class AllowEmptyOption : Option<bool> {
		public AllowEmptyOption(string name, params string[] aliases) : base(name, aliases) {
			Description = "Accept an empty answer instead of asking again";
		}
	}
}