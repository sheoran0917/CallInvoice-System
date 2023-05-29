using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Arbolus.CallInvoice.Models
{
	public class Rate
	{
		/// <summary>
		/// Currency conversion rates.
		/// </summary>
		[JsonProperty(PropertyName ="AUD")]
		public Dictionary<string, decimal> AUD { get; set; }

		[JsonProperty(PropertyName = "CAD")]
		public Dictionary<string, decimal> CAD { get; set; }

		[JsonProperty(PropertyName = "EUR")]
		public Dictionary<string, decimal> EUR { get; set; }

		[JsonProperty(PropertyName = "GBP")]
		public Dictionary<string, decimal> GBP { get; set; }

		[JsonProperty(PropertyName = "JPY")]
		public Dictionary<string, decimal> JPY { get; set; }

		[JsonProperty(PropertyName = "USD")]
		public Dictionary<string, decimal> USD { get; set; }

	}
}
