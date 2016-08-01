using System;
using System.Text.RegularExpressions;

namespace Paraesthesia.Applications.GuidGenConsole
{
	public class Options
	{
		private static readonly Regex ArgParser = new Regex(@"^\/(?<name>[\w\?])(\s*=\s*(?<value>[\d\w]+))?$", RegexOptions.ExplicitCapture);

		public string Format { get; set; } = GuidFormat.FormatDefault;

		public int Quantity { get; set; } = 1;

		/// <summary>
		/// Parse command line arguments.
		/// </summary>
		/// <param name="args">The set of arguments to parse.</param>
		/// <exception cref="System.ArgumentException">
		/// Thrown if there is any problem parsing arguments and indicates help
		/// should be displayed.
		/// </exception>
		public static Options Parse(string[] args)
		{
			var options = new Options();

			// No args to parse
			if (args == null || args.Length == 0)
			{
				return options;
			}

			// Set up parser

			for (var i = 0; i < args.Length; i++)
			{
				// Get the name and optional value for each argument
				var parsed = ArgParser.Match(args[i]);
				if (parsed == null || !parsed.Success)
				{
					throw new ArgumentException(string.Format("Incorrect argument format: {0}", args[i]));
				}

				switch (parsed.Groups["name"].Value)
				{
					case "f":
						// Format
						switch (parsed.Groups["value"].Value)
						{
							case "o":
								options.Format = GuidFormat.FormatImplementOleCreate;
								break;
							case "d":
								options.Format = GuidFormat.FormatDefineGuid;
								break;
							case "s":
								options.Format = GuidFormat.FormatStaticConstStruct;
								break;
							case "r":
								options.Format = GuidFormat.FormatRegistry;
								break;
							default:
								throw new ArgumentException(string.Format("Unknown/unspecified format type: {0}", parsed.Groups["value"].Value));
						}
						break;

					case "q":
						// Quantity
						try
						{
							options.Quantity = int.Parse(parsed.Groups["value"].Value);
							if (options.Quantity < 1)
							{
								options.Quantity = 1;
							}
						}
						catch (Exception)
						{
							throw new ArgumentException(string.Format("Invalid quantity specified: {0}", parsed.Groups["value"].Value));
						}
						break;

					case "?":
					default:
						// Display help
						throw new ArgumentException("");
				}
			}

			return options;
		}
	}
}
