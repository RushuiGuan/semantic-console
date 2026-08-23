using Albatross.CommandLine.Annotations;
using System.CommandLine;

namespace Albatross.SemanticConsole.Inputs {
	[DefaultNameAliases("--default", "-d")]
	public class DefaultOption<T> : Option<T> {
		public DefaultOption(string name, params string[] aliases) : base(name, aliases) {
			Description = "Default prompt value";
		}
	}
}