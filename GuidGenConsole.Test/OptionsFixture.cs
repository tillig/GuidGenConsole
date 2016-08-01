using System;
using System.Linq;
using Xunit;

namespace Paraesthesia.Applications.GuidGenConsole.Test
{
	public class OptionsFixture
	{
		[Fact]
		public void Parse_EmptyArgs()
		{
			var options = Options.Parse(new string[0]);
			Assert.Equal(1, options.Quantity);
			Assert.Equal(GuidFormat.FormatDefault, options.Format);
		}

		[Theory]
		[InlineData("/f=o", GuidFormat.FormatImplementOleCreate)]
		[InlineData("/f=d", GuidFormat.FormatDefineGuid)]
		[InlineData("/f=s", GuidFormat.FormatStaticConstStruct)]
		[InlineData("/f=r", GuidFormat.FormatRegistry)]
		public void Parse_Format(string arg, string expected)
		{
			var options = Options.Parse(new string[] { arg });
			Assert.Equal(expected, options.Format);
		}

		[Fact]
		public void Parse_NullArgs()
		{
			var options = Options.Parse(null);
			Assert.Equal(1, options.Quantity);
			Assert.Equal(GuidFormat.FormatDefault, options.Format);
		}

		[Theory]
		[InlineData("/q=-1", 1)]
		[InlineData("/q=0", 1)]
		[InlineData("/q=1", 1)]
		[InlineData("/q=10", 10)]
		public void Parse_Quantity(string arg, int expected)
		{
			var options = Options.Parse(new string[] { arg });
			Assert.Equal(expected, options.Quantity);
		}
	}
}
