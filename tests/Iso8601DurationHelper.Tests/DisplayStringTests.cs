using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Iso8601DurationHelper.Tests
{
	public class DisplayStringTests
	{

		[Theory]
		[InlineData("P1Y", "1 Year")]
		[InlineData("P2Y", "2 Years")]
		[InlineData("P1M", "1 Month")]
		[InlineData("P2M", "2 Months")]
		[InlineData("P1W", "1 Week")]
		[InlineData("P2W", "2 Weeks")]
		[InlineData("P1D", "1 Day")]
		[InlineData("P2D", "2 Days")]
		[InlineData("PT1H", "1 Hour")]
		[InlineData("PT2H", "2 Hours")]
		[InlineData("PT1M", "1 Minute")]
		[InlineData("PT2M", "2 Minutes")]
		[InlineData("PT1S", "1 Second")]
		[InlineData("PT2S", "2 Seconds")]
		public void DisplaySimpleDurationEnglish(string input, string expected)
		{
			Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
			var duration = Duration.Parse(input);
			Assert.Equal(expected, duration.ToDisplayString(false));
		}

		[Theory]
		[InlineData("P1Y", "1 Año")]
		[InlineData("P2Y", "2 Años")]
		[InlineData("P1M", "1 Mes")]
		[InlineData("P2M", "2 Meses")]
		[InlineData("P1W", "1 Semana")]
		[InlineData("P2W", "2 Semanas")]
		[InlineData("P1D", "1 Dia")]
		[InlineData("P2D", "2 Dias")]
		[InlineData("PT1H", "1 Hora")]
		[InlineData("PT2H", "2 Horas")]
		[InlineData("PT1M", "1 Minuto")]
		[InlineData("PT2M", "2 Minutos")]
		[InlineData("PT1S", "1 Segundo")]
		[InlineData("PT2S", "2 Segundos")]
		public void DisplaySimpleDurationSpanish(string input, string expected)
		{
			Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-MX");
			var duration = Duration.Parse(input);
			Assert.Equal(expected, duration.ToDisplayString(false));
		}

		[Theory]
		[InlineData("P1Y", "1 Année")]
		[InlineData("P2Y", "2 Ans")]
		[InlineData("P1M", "1 Mois")]
		[InlineData("P2M", "2 Mois")]
		[InlineData("P1W", "1 Semaine")]
		[InlineData("P2W", "2 Semaines")]
		[InlineData("P1D", "1 Jour")]
		[InlineData("P2D", "2 Jours")]
		[InlineData("PT1H", "1 Heure")]
		[InlineData("PT2H", "2 Heures")]
		[InlineData("PT1M", "1 Minute")]
		[InlineData("PT2M", "2 Minutes")]
		[InlineData("PT1S", "1 Seconde")]
		[InlineData("PT2S", "2 Secondes")]
		public void DisplaySimpleDurationFrench(string input, string expected)
		{
			Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = new CultureInfo("fr-CA");
			var duration = Duration.Parse(input);
			Assert.Equal(expected, duration.ToDisplayString(false));
		}

		[Theory]
		[InlineData("P1Y2M", "1 Year, 2 Months")]
		[InlineData("P2M3DT4H", "2 Months, 3 Days, 4 Hours")]
		[InlineData("PT5M6S", "5 Minutes, 6 Seconds")]
		[InlineData("P7Y8M9DT1H2M3S", "7 Years, 8 Months, 9 Days, 1 Hour, 2 Minutes, 3 Seconds")]
		public void DisplayComplexEnglish(string input, string expected)
		{
			Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
			var duration = Duration.Parse(input);
			Assert.Equal(expected, duration.ToDisplayString(false));
		}

		[Theory]
		[InlineData("PT1H", "1:00:00")]
		[InlineData("PT1M", "0:01:00")]
		[InlineData("PT1S", "0:00:01")]
		[InlineData("PT2H3M4S", "2:03:04")]
		[InlineData("PT10H11M12S", "10:11:12")]
		public void DisplayNumericTime(string input, string expected)
		{
			var duration = Duration.Parse(input);
			Assert.Equal(expected, duration.ToDisplayString());
		}

		[Theory]
		[InlineData("P1DT1H", "1 Day, 1 Hour")]
		[InlineData("PT25H", "1 Day, 1 Hour")]
		[InlineData("PT23H59M60S", "1 Day")]
		public void NumericTimeOverride(string input, string expected)
		{
			Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
			var duration = Duration.Parse(input);
			Assert.Equal(expected, duration.ToDisplayString());
		}

		[Theory]
		[InlineData("P1Y1M", 2, "1.08 Years")]
		[InlineData("P2M3W", 3, "2.7 Months")]
		[InlineData("P4DT5H", 4, "4.2083 Days")]
		[InlineData("P6Y7M8W9DT10H11M12S", 5, "6.76258 Years")]
		public void DecimalOutput(string input, byte precision, string expected)
		{
			Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
			var duration = Duration.Parse(input);
			Assert.Equal(expected, duration.ToDisplayString(false, true, precision));
		}
	}
}
