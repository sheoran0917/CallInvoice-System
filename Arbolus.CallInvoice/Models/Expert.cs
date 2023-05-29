using Newtonsoft.Json;

namespace Arbolus.CallInvoice.Models
{
	public class Expert
	{
		/// <summary>
		/// List of calls made by the expert
		/// </summary>
		[JsonProperty(PropertyName = "calls")]
		public List<Call> Calls { get; set; }

		/// <summary>
		/// Currency in which the expert is paid
		/// </summary>
		[JsonProperty(PropertyName = "currency")]
		public string Currency { get; set; }

		/// <summary>
		/// Hourly rate of the expert
		/// </summary>
		[JsonProperty(PropertyName = "hourlyRate")]
		public decimal HourlyRate { get; set; }

		/// <summary>
		/// Name of the expert
		/// </summary>
		[JsonProperty(PropertyName = "name")]
		public string Name { get; set; }
	}
}
