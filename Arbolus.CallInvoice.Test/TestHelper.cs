using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arbolus.CallInvoice.Tests
{
	public static class TestHelper
	{
		public static IConfiguration GetConfiguration()
		{
			var configuration = new ConfigurationBuilder()
				.AddInMemoryCollection(new Dictionary<string, string?>
				{
				{ "Discounts:1HourAgreement", "1 hour agreement" },
				{ "Discounts:Follow", "Follow" },
				{ "SourceCurrency", "USD" }
				})
				.Build();

			return configuration;
		}
	}
}
