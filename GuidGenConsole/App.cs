using System;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using CommandLine;

namespace Paraesthesia.Applications.GuidGenConsole
{
	/// <summary>
	/// GUID-generating console application
	/// </summary>
	public static class App
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		public static int Main(string[] args)
		{
			return Parser.Default.ParseArguments<Options>(args)
				.MapResult(
					options => Main(options),
					_ => 1);
		}

		public static int Main(Options options)
		{
			options.Normalize();

			// Prepare for clipboard storage
			var output = new string[options.Quantity];

			// Create the desired number of GUIDs
			for (var i = 0; i < options.Quantity; i++)
			{
				// Create the GUID and get commonly used formatted elements
				var guid = Guid.NewGuid();
				var guidBytes = guid.ToByteArray();
				var guidBlocks = new string[guidBytes.Length];
				for (var j = 0; j < guidBytes.Length; j++)
				{
					guidBlocks[j] = guidBytes[j].ToString("x2").ToLower();
				}

				// Format and display the GUID
				var toDisplay = string.Format(
					new GuidFormatProvider(CultureInfo.CurrentCulture),
					options.FormatString,
					guid,
					guidBlocks[0],
					guidBlocks[1],
					guidBlocks[2],
					guidBlocks[3],
					guidBlocks[4],
					guidBlocks[5],
					guidBlocks[6],
					guidBlocks[7],
					guidBlocks[8],
					guidBlocks[9],
					guidBlocks[10],
					guidBlocks[11],
					guidBlocks[12],
					guidBlocks[13],
					guidBlocks[14],
					guidBlocks[15]);
				Console.WriteLine(toDisplay);
				output[i] = toDisplay;
			}

			// Save the output to the clipboard
			Clipboard.SetDataObject(string.Join(Environment.NewLine, output), true);
			return 0;
		}
	}
}
