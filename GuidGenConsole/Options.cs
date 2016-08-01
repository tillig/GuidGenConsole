using System;
using System.Collections.Generic;
using System.Linq;
using CommandLine;
using CommandLine.Text;

namespace Paraesthesia.Applications.GuidGenConsole
{
	public class Options
	{
		private const string FormatHelp = @"The GUID format to generate. Options include:
ole - IMPLEMENT_OLECREATE(...)
def - DEFINE_GUID(...)
struct - static const struct Guid = {{...}}
reg - Registry Format
custom - Custom format (specify /s with the format)
If omitted, the GUID will be unformatted.";

		[Option('f', "format", HelpText = FormatHelp)]
		public GuidFormat Format { get; set; }

		[Option('s', HelpText = "If 'format' is 'custom,' this is the custom format string. Use String.Format style.")]
		public string FormatString { get; set; }

		[Option('q', "quantity", HelpText = "The number of GUIDs to generate.")]
		public int Quantity { get; set; }

		[Usage(ApplicationAlias = "guidgenconsole")]
		public static IEnumerable<Example> Examples
		{
			get
			{
				yield return new Example("Unformatted GUID", new Options());
				yield return new Example("Two registry format GUIDs", new Options { Format = GuidFormat.reg, Quantity = 2 });
				yield return new Example("Custom format GUID", new Options { Format = GuidFormat.custom, FormatString = "{0:N}" });
			}
		}

		public void Normalize()
		{
			if (string.IsNullOrEmpty(this.FormatString))
			{
				this.FormatString = GuidFormatStrings.FormatDefault;
			}

			if (this.Format != GuidFormat.custom)
			{
				switch (this.Format)
				{
					case GuidFormat.def:
						this.FormatString = GuidFormatStrings.FormatDefineGuid;
						break;
					case GuidFormat.ole:
						this.FormatString = GuidFormatStrings.FormatImplementOleCreate;
						break;
					case GuidFormat.reg:
						this.FormatString = GuidFormatStrings.FormatRegistry;
						break;
					case GuidFormat.@struct:
						this.FormatString = GuidFormatStrings.FormatStaticConstStruct;
						break;
				}
			}

			if (this.Quantity < 1)
			{
				this.Quantity = 1;
			}
		}

		public void GetUsage()
		{

			//var asmName = Assembly.GetExecutingAssembly().GetName();
			//output.WriteLine("{0} v{1}", asmName.Name, asmName.Version);
			//output.WriteLine("Generates GUIDs in various formats and copies to the clipboard.");
			//output.WriteLine("{0} [/f=format] [/q=quantity]", asmName.Name);
			//output.WriteLine("{0} [/?]\n", asmName.Name);
			//output.WriteLine("Parameters:");
			//output.WriteLine("format: The GUID format to generate.  Valid choices are...");
			//output.WriteLine("    o - IMPLEMENT_OLECREATE(...)");
			//output.WriteLine("    d - DEFINE_GUID(...)");
			//output.WriteLine("    s - static const struct Guid = {{...}}");
			//output.WriteLine("    r - Registry Format");
			//output.WriteLine("    If omitted, the GUID will be unformatted.");
			//output.WriteLine("quantity: The number of GUIDs to generate. Defaults to 1.\n");
			//output.WriteLine("?: Displays this help text.\n");
			//output.WriteLine("Example:");
			//output.WriteLine("{0} /f=r /q=3\n", asmName.Name);
		}
	}
}
