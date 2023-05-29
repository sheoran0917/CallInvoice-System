using Arbolus.CallInvoice.Models;

namespace Arbolus.CallInvoice.BusinessLogic.Services
{
    public interface IRatesService
    {
        /// <summary>
        /// Get all the rates data from endpoint.
        /// </summary>
        /// <returns></returns>
        Task<Rate> GetRates();

        /// <summary>
        /// Get the target currency value on the basis of source currency set.
        /// </summary>
        /// <param name="sourceCurrency"></param>
        /// <param name="targetCurrency"></param>
        /// <param name="sourceCurrencyValue"></param>
        /// <returns></returns>
		decimal GetTargetCurrencyValue(Rate rates,string sourceCurrency, string targetCurrency, decimal sourceCurrencyValue);
	}
}
