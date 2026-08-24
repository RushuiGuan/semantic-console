using Albatross.SemanticConsole.Enums;
using Spectre.Console;
using System.Text;

namespace Albatross.SemanticConsole.Spectre {
	public static class Extensions {
		public const int IndentSize = 2;
		/// <summary>
		/// The same indent as <see cref="Indent"/>, as padding a renderable is written with. Padding is
		/// applied to every line the text wraps onto, so a continuation starts at the column its own text
		/// starts at rather than back at the run scope.
		/// </summary>
		public static Padding ToPadding(this Level level) => new Padding(((int)level - 1) * IndentSize, 0, 0, 0);
	}
}