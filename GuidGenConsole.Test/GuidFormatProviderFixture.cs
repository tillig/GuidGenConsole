using System;
using System.Globalization;
using System.Linq;
using Xunit;

namespace Paraesthesia.Applications.GuidGenConsole.Test
{
	public class GuidFormatProviderFixture
	{
		[Theory]
		[InlineData("{0:N}", "fb25a80b78ce42aba963753699314838")]
		[InlineData("{0:NL}", "fb25a80b78ce42aba963753699314838")]
		[InlineData("{0:NU}", "FB25A80B78CE42ABA963753699314838")]
		[InlineData("{0}", "fb25a80b-78ce-42ab-a963-753699314838")]
		[InlineData("{0:D}", "fb25a80b-78ce-42ab-a963-753699314838")]
		[InlineData("{0:DU}", "FB25A80B-78CE-42AB-A963-753699314838")]
		public void Format_HandlesFormatStrings(string format, string expected)
		{
			var guid = new Guid("fb25a80b-78ce-42ab-a963-753699314838");
			var actual = string.Format(new GuidFormatProvider(CultureInfo.CurrentCulture), format, guid);
			Assert.Equal(expected, actual);
		}
	}
}
