using System;
using System.Collections.Generic;
using System.Linq;
using CommandLine;
using CommandLine.Text;

namespace Paraesthesia.Applications.GuidGenConsole
{
	public class Options
	{
		[Option('f', "format", HelpText = "The GUID format to generate. If omitted, the GUID will be unformatted.")]
		public GuidFormat Format { get; set; }

		[Option('s', HelpText = "If 'format' is 'custom,' this is the custom format string. Use String.Format style ({0:NDBPXUL}) - U/L indicates upper/lower case.")]
		public string FormatString { get; set; }

		[Option('q', "quantity", HelpText = "The number of GUIDs to generate.")]
		public int Quantity { get; set; }

		[Usage(ApplicationAlias = "guidgenconsole")]
		public static IEnumerable<Example> Examples
		{
			get
			{
				yield return new Example("Unformatted GUID", new Options());
				yield return new Example("Custom format", new Options { Format = GuidFormat.custom, FormatString = "{0:NU}" });
				yield return new Example("Registry {xxxxxxxx-xxxx-....}", new Options { Format = GuidFormat.reg });
				yield return new Example("IMPLEMENT_OLECREATE(...)", new Options { Format = GuidFormat.ole });
				yield return new Example("DEFINE_GUID(...)", new Options { Format = GuidFormat.def });
				yield return new Example("static const struct Guid = {{...}}", new Options { Format = GuidFormat.@struct });
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
	}
}
