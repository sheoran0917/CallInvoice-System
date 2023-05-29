using Arbolus.CallInvoice.Models;
using Arbolus.CallInvoice.Common;

namespace Arbolus.CallInvoice.BusinessLogic.Services
{
    public class RatesService : IRatesService
    {
		private readonly ILogger<ClientService> _logger;
		private readonly IConfiguration _configuration;
		private readonly DataRetriever _dataRetriever;

		public RatesService(ILogger<ClientService> logger, IConfiguration configuration, DataRetriever dataRetriever)
		{
			_logger = logger;
			_configuration = configuration;
			_dataRetriever = dataRetriever;
		}
	
		public async Task<Rate> GetRates()
        {
			try
			{
				var ratesData = await _dataRetriever.GetData<Rate>(_configuration.GetSection("EndPoints:RateEndpoint").Value ?? "");
				return ratesData;
			}
			catch (Exception ex)
			{

				throw;
			}
        }

		public decimal GetTargetCurrencyValue(Rate rates,string sourceCurrency, string targetCurrency, decimal sourceCurrencyValue)
		{
			if(sourceCurrency == targetCurrency)
			{
				return sourceCurrencyValue;
			}
			if (rates != null)
			{
				decimal targetRate = GetCurrencyRate(rates, targetCurrency, sourceCurrency);
	
				decimal targetCurrencyValue = sourceCurrencyValue / targetRate;
				return Math.Round(targetCurrencyValue,2);
			}
			else
			{
				throw new ArgumentNullException("rates");
			}
		}
		private decimal GetCurrencyRate(Rate rates, string currency, string targetCurrency)
		{
			decimal rate = 0m;
			switch (currency)
			{
				case "AUD":
					if (rates.AUD.TryGetValue(targetCurrency, out rate))
						return rate;
					break;
				case "CAD":
					if (rates.CAD.TryGetValue(targetCurrency, out rate))
						return rate;
					break;
				case "EUR":
					if (rates.EUR.TryGetValue(targetCurrency, out rate))
						return rate;
					break;
				case "GBP":
					if (rates.GBP.TryGetValue(targetCurrency, out rate))
						return rate;
					break;
				case "JPY":
					if (rates.JPY.TryGetValue(targetCurrency, out rate))
						return rate;
					break;
				case "USD":
					if (rates.USD.TryGetValue(targetCurrency, out rate))
						return rate;
					break;
			}

			throw new ArgumentException("Invalid currency or target currency does not exist.");
		}
	}
}
