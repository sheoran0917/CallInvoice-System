using Arbolus.CallInvoice.Models;

namespace Arbolus.CallInvoice.BusinessLogic
{
	public interface IPriceCalculator
	{
		/// <summary>
		/// Calcaulate each call price.
		/// </summary>
		/// <returns></returns>
		Task<List<CallPrice>> CalculateEachCallPrice();
	}
}
