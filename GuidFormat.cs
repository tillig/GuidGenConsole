using System;

namespace Paraesthesia.Applications.GuidGenConsole
{
	public static class GuidFormat
	{
		/// <summary>
		/// Default GUID format.
		/// </summary>
		public const string FormatDefault = "{0}";

		/// <summary>
		/// Format for DEFINE_GUID(...)
		/// </summary>
		public const string FormatDefineGuid = "// {{{0}}}\nDEFINE_GUID(<<name>>, 0x{4}{3}{2}{1}, 0x{6}{5}, 0x{8}{7}, 0x{9}, 0x{10}, 0x{11}, 0x{12}, 0x{13}, 0x{14}, 0x{15}, 0x{16});";

		/// <summary>
		/// Format for IMPLEMENT_OLECREATE(...)
		/// </summary>
		public const string FormatImplementOleCreate = "// {{{0}}}\nIMPLEMENT_OLECREATE(<<class>>, <<external_name>>, 0x{4}{3}{2}{1}, 0x{6}{5}, 0x{8}{7}, 0x{9}, 0x{10}, 0x{11}, 0x{12}, 0x{13}, 0x{14}, 0x{15}, 0x{16});";

		/// <summary>
		/// Registry GUID format.
		/// </summary>
		public const string FormatRegistry = "{0:B}";

		/// <summary>
		/// Format for static const struct GUID = {...}
		/// </summary>
		public const string FormatStaticConstStruct = "// {{{0}}}\nstatic const GUID <<name>> = {{ 0x{4}{3}{2}{1}, 0x{6}{5}, 0x{8}{7}, {{ 0x{9}, 0x{10}, 0x{11}, 0x{12}, 0x{13}, 0x{14}, 0x{15}, 0x{16} }} }};";
	}
}
