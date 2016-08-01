using System;
using System.IO;
using System.Reflection;

namespace Paraesthesia.Applications.GuidGenConsole
{
	public static class Help
	{
		/// <summary>
		/// Displays command usage.
		/// </summary>
		public static void Show(TextWriter output)
		{
			var asmName = Assembly.GetExecutingAssembly().GetName();
			output.WriteLine("{0} v{1}", asmName.Name, asmName.Version);
			output.WriteLine("Generates GUIDs in various formats and copies to the clipboard.");
			output.WriteLine("{0} [/f=format] [/q=quantity]", asmName.Name);
			output.WriteLine("{0} [/?]\n", asmName.Name);
			output.WriteLine("Parameters:");
			output.WriteLine("format: The GUID format to generate.  Valid choices are...");
			output.WriteLine("    o - IMPLEMENT_OLECREATE(...)");
			output.WriteLine("    d - DEFINE_GUID(...)");
			output.WriteLine("    s - static const struct Guid = {{...}}");
			output.WriteLine("    r - Registry Format");
			output.WriteLine("    If omitted, the GUID will be unformatted.");
			output.WriteLine("quantity: The number of GUIDs to generate. Defaults to 1.\n");
			output.WriteLine("?: Displays this help text.\n");
			output.WriteLine("Example:");
			output.WriteLine("{0} /f=r /q=3\n", asmName.Name);
		}
	}
}
