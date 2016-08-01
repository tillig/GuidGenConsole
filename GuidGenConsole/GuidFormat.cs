using System;
using System.Linq;

namespace Paraesthesia.Applications.GuidGenConsole
{
	public enum GuidFormat
	{
		/// <summary>
		/// Custom string format.
		/// </summary>
		custom,

		/// <summary>
		/// DEFINE_GUID(...)
		/// </summary>
		def,

		/// <summary>
		/// IMPLEMENT_OLECREATE(...)
		/// </summary>
		ole,

		/// <summary>
		/// Registry GUID format.
		/// </summary>
		reg,

		/// <summary>
		/// static const struct GUID = {...}
		/// </summary>
		@struct
	}
}
