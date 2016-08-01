using System;
using System.Linq;

namespace Paraesthesia.Applications.GuidGenConsole
{
	public class GuidFormatProvider : IFormatProvider, ICustomFormatter
	{
		private readonly IFormatProvider _baseProvider;

		public GuidFormatProvider(IFormatProvider baseProvider)
		{
			this._baseProvider = baseProvider;
		}

		public string Format(string format, object arg, IFormatProvider formatProvider)
		{
			if (format == null)
			{
				return String.Format("{0}", arg);
			}

			char? modifier = null;
			if(format.EndsWith("U") || format.EndsWith("L"))
			{
				modifier = format[format.Length - 1];
				format = format.TrimEnd('U', 'L');
			}

			string result = null;
			if (arg is IFormattable)
			{
				result = ((IFormattable)arg).ToString(format, formatProvider);
			}
			else if (arg != null)
			{
				result = arg.ToString();
			}

			if(modifier != null && result != null)
			{
				switch(modifier)
				{
					case 'U':
						result = result.ToUpperInvariant();
						break;
					case 'L':
						result = result.ToLowerInvariant();
						break;
				}
			}

			return result;
		}

		public object GetFormat(Type formatType)
		{
			if (formatType == typeof(ICustomFormatter))
			{
				return this;
			}

			return null;
		}
	}
}
