using System;
using System.Linq;
using CommandLine;
using Xunit;

namespace Paraesthesia.Applications.GuidGenConsole.Test
{
	public class OptionsFixture
	{
		[Fact]
		public void Parse_EmptyArgs()
		{
			var result = Parser.Default.ParseArguments<Options>(new string[0]);
			result.MapResult(
				options =>
				{
					Assert.Equal(0, options.Quantity);
					Assert.Equal(null, options.FormatString);
					return 0;
				},
				options => { throw new Exception("Options were not parsed."); });
		}

		[Theory]
		[InlineData("-f ole", GuidFormatStrings.FormatImplementOleCreate)]
		[InlineData("-f def", GuidFormatStrings.FormatDefineGuid)]
		[InlineData("-f struct", GuidFormatStrings.FormatStaticConstStruct)]
		[InlineData("-f reg", GuidFormatStrings.FormatRegistry)]
		[InlineData("-f custom", GuidFormatStrings.FormatDefault)]
		public void Parse_Format(string arg, string expectedFormatString)
		{
			var result = Parser.Default.ParseArguments<Options>(new string[] { arg });
			result.MapResult(
				options => { options.Normalize(); Assert.Equal(expectedFormatString, options.FormatString); return 0; },
				options => { throw new Exception("Options were not parsed."); });
		}

		[Theory]
		[InlineData("-q -1", 1)]
		[InlineData("-q 0", 1)]
		[InlineData("-q 1", 1)]
		[InlineData("-q 10", 10)]
		public void Parse_Quantity(string arg, int expected)
		{
			var result = Parser.Default.ParseArguments<Options>(new string[] { arg });
			result.MapResult(
				options => { options.Normalize(); Assert.Equal(expected, options.Quantity); return 0; },
				options => { throw new Exception("Options were not parsed."); });
		}
	}
}
