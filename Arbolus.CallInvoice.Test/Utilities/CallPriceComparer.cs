using Arbolus.CallInvoice.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

public class CallPriceComparer : IEqualityComparer<CallPrice>
{
	public bool Equals(CallPrice x, CallPrice y)
	{
		// Convert the objects to JSON strings and compare them
		string xJson = JsonConvert.SerializeObject(x);
		string yJson = JsonConvert.SerializeObject(y);
		return xJson == yJson;
	}

	public int GetHashCode(CallPrice obj)
	{
		// Compute a hash code based on the JSON string of the object
		string json = JsonConvert.SerializeObject(obj);
		return json.GetHashCode();
	}
}
