using System;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Paraesthesia.Applications.GuidGenConsole
{
	/// <summary>
	/// GUID-generating console application
	/// </summary>
	class App
	{
		/// <summary>
		/// Format for IMPLEMENT_OLECREATE(...)
		/// </summary>
		public const string FormatImplementOleCreate = "// {{{0}}}\nIMPLEMENT_OLECREATE(<<class>>, <<external_name>>, 0x{4}{3}{2}{1}, 0x{6}{5}, 0x{8}{7}, 0x{9}, 0x{10}, 0x{11}, 0x{12}, 0x{13}, 0x{14}, 0x{15}, 0x{16});";

		/// <summary>
		/// Format for DEFINE_GUID(...)
		/// </summary>
		public const string FormatDefineGuid = "// {{{0}}}\nDEFINE_GUID(<<name>>, 0x{4}{3}{2}{1}, 0x{6}{5}, 0x{8}{7}, 0x{9}, 0x{10}, 0x{11}, 0x{12}, 0x{13}, 0x{14}, 0x{15}, 0x{16});";
		
		/// <summary>
		/// Format for static const struct GUID = {...}
		/// </summary>
		public const string FormatStaticConstStruct = "// {{{0}}}\nstatic const GUID <<name>> = {{ 0x{4}{3}{2}{1}, 0x{6}{5}, 0x{8}{7}, {{ 0x{9}, 0x{10}, 0x{11}, 0x{12}, 0x{13}, 0x{14}, 0x{15}, 0x{16} }} }};";

		/// <summary>
		/// Registry GUID format.
		/// </summary>
		public const string FormatRegistry = "{0:B}";

		/// <summary>
		/// Default GUID format.
		/// </summary>
		public const string FormatDefault = "{0}";

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			// Parse arguments and set values for processing
			string format;
			int quantity;

			try
			{
				ParseArguments(args, out format, out quantity);
			}
			catch(ArgumentException err)
			{
				Console.WriteLine(err.Message);
				ShowHelp();
				return;
			}

			// Prepare for clipboard storage
			string[] output = new string[quantity];

			// Create the desired number of GUIDs
			for(int i = 0; i < quantity; i++)
			{
				// Create the GUID and get commonly used formatted elements
				Guid guid = Guid.NewGuid();
				byte[] guidBytes = guid.ToByteArray();
				string[] guidBlocks = new string[guidBytes.Length];
				for(int j = 0; j < guidBytes.Length; j++)
				{
					guidBlocks[j] = guidBytes[j].ToString("x2").ToLower();
				}

				// Format and display the GUID
				String toDisplay = String.Format(
					format,
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
			Clipboard.SetDataObject(String.Join("\n", output), true);

			Console.WriteLine("");
		}

		/// <summary>
		/// Parse command line arguments.
		/// </summary>
		/// <param name="args">The set of arguments to parse.</param>
		/// <param name="format">The format to use when displaying the GUID.</param>
		/// <param name="quantity">The number of GUIDs to generate.</param>
		/// <exception cref="System.ArgumentException">
		/// Thrown if there is any problem parsing arguments and indicates help
		/// should be displayed.
		/// </exception>
		/// <seealso cref="Paraesthesia.Applications.GuidGenConsole.App" />
		static void ParseArguments(string[] args, out string format, out int quantity)
		{
			// Set defaults
			format = FormatDefault;
			quantity = 1;

			// No args to parse
			if(args == null || args.Length == 0)
			{
				return;
			}

			// Set up parser
			Regex argParser = new Regex(@"^\/(?<name>[\w\?])(\s*=\s*(?<value>[\d\w]+))?$", RegexOptions.ExplicitCapture);

			for(int i = 0; i < args.Length; i++)
			{
				// Get the name and optional value for each argument
				Match parsed = argParser.Match(args[i]);
				if(parsed == null || !parsed.Success)
				{
					throw new ArgumentException(String.Format("Incorrect argument format: {0}", args[i]));
				}

				switch (parsed.Groups["name"].Value)
				{
					case "f":
						// Format
					switch(parsed.Groups["value"].Value)
					{
						case "o":
							format = App.FormatImplementOleCreate;
							break;
						case "d":
							format = App.FormatDefineGuid;
							break;
						case "s":
							format = App.FormatStaticConstStruct;
							break;
						case "r":
							format = App.FormatRegistry;
							break;
						default:
							throw new ArgumentException(String.Format("Unknown/unspecified format type: {0}", parsed.Groups["value"].Value));
					}
						break;
					
					case "q":
						// Quantity
						try
						{
							quantity = Int32.Parse(parsed.Groups["value"].Value);
							if(quantity < 1)
							{
								quantity = 1;
							}
						}
						catch(Exception)
						{
							throw new ArgumentException(String.Format("Invalid quantity specified: {0}", parsed.Groups["value"].Value));
						}
						break;

					case "?":
					default:
						// Display help
						throw new ArgumentException("");
				}
			}
		}

		/// <summary>
		/// Displays command usage.
		/// </summary>
		/// <seealso cref="Paraesthesia.Applications.GuidGenConsole.App" />
		static void ShowHelp()
		{
			AssemblyName asmName = Assembly.GetExecutingAssembly().GetName();
			Console.WriteLine("{0} v{1}", asmName.Name, asmName.Version);
			Console.WriteLine("Generates GUIDs in various formats and copies to the clipboard.");
			Console.WriteLine("{0} [/f=format] [/q=quantity]", asmName.Name);
			Console.WriteLine("{0} [/?]\n", asmName.Name);
			Console.WriteLine("Parameters:");
			Console.WriteLine("format: The GUID format to generate.  Valid choices are...");
			Console.WriteLine("    o - IMPLEMENT_OLECREATE(...)");
			Console.WriteLine("    d - DEFINE_GUID(...)");
			Console.WriteLine("    s - static const struct Guid = {{...}}");
			Console.WriteLine("    r - Registry Format");
			Console.WriteLine("    If omitted, the GUID will be unformatted.");
			Console.WriteLine("quantity: The number of GUIDs to generate. Defaults to 1.\n");
			Console.WriteLine("?: Displays this help text.\n");
			Console.WriteLine("Example:");
			Console.WriteLine("{0} /f=r /q=3\n", asmName.Name);
		}
	}
}
