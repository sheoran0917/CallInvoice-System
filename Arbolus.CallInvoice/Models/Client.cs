using Newtonsoft.Json;

namespace Arbolus.CallInvoice.Models
{
	public class Client
	{
		/// <summary>
		/// The name of the client.
		/// </summary>
		[JsonProperty(PropertyName = "name")]
		public string Name { get; set; }

		/// <summary>
		/// The list of discounts applicable to the client.
		/// </summary>
		[JsonProperty(PropertyName = "discounts", NullValueHandling = NullValueHandling.Ignore)]
		public List<string> Discounts { get; set; }
	}
}
