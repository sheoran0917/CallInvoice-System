using Newtonsoft.Json;

namespace Arbolus.CallInvoice.Models
{
	public class ClientData
	{
		/// <summary>
		/// List of all the clients.
		/// </summary>
		[JsonProperty(PropertyName = "Clients")]
		public List<Client> Clients { get; set; }
	}
}
