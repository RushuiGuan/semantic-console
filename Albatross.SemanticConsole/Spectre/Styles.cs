using Spectre.Console;
using Status = Albatross.SemanticConsole.Enums.Status;

namespace Albatross.SemanticConsole.Spectre {
	/// <summary>
	/// The sixteen colour palette entries a PowerShell installer writes with, so a run reads the same
	/// whichever one produced it.
	/// </summary>
	public static class Styles {
		public static Style Header { get; } = new Style(Color.Aqua);
		public static Style Info { get; } = new Style(Color.Grey);
		public static Style Warning { get; } = new Style(Color.Yellow);
		public static Style Error { get; } = new Style(Color.Red);
		public static Style Success { get; } = new Style(Color.Lime);
		public static Style Content { get; } = new Style(Color.Gray100);

		/// <summary>
		/// Every status renders at every level. <paramref name="defaultStyle"/> carries what the element
		/// means when it claims no status — a heading for an action, a receipt for an info.
		/// </summary>
		public static Style GetStyle(this Status status, Style defaultStyle) => status switch {
			Status.Success => Success,
			Status.Warning => Warning,
			Status.Error => Error,
			_ => defaultStyle,
		};
	}
}