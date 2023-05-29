using Newtonsoft.Json;

namespace Arbolus.CallInvoice.Models
{
	public class Call
	{
		/// <summary>
		/// The client associated with the call.
		/// </summary>
		[JsonProperty(PropertyName = "client")]
		public string Client { get; set; }

		/// <summary>
		/// Represents the duration of the call in minutes.
		/// </summary>
		[JsonProperty(PropertyName = "duration")]
		public int Duration { get; set; }
	}
}
