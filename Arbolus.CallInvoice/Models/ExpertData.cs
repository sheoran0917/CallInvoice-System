using Newtonsoft.Json;

namespace Arbolus.CallInvoice.Models
{
	public class ExpertData
	{
		/// <summary>
		/// List of all the experts.
		/// </summary>
		[JsonProperty(PropertyName = "Experts")]
		public List<Expert> Experts { get; set; }
	}
}
