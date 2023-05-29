using Arbolus.CallInvoice.BusinessLogic.Services;
using Arbolus.CallInvoice.Models;

namespace Arbolus.CallInvoice.BusinessLogic
{
	public class PriceCalculator : IPriceCalculator
	{
		private readonly IClientsService _clientsService;
		private readonly IExpertsService _expertsService;
		private readonly IRatesService _ratesService;
		private readonly IConfiguration _configuration;
		private readonly ILogger<PriceCalculator> _logger;
		private const string DISCOUNTFOLLOW = "Follow";
		private const string DISCOUNTONEHOURAGREEMENT = "1 hour agreement";

		public PriceCalculator(IClientsService clientsService, IExpertsService expertsService, IRatesService ratesService, IConfiguration configuration, ILogger<PriceCalculator> logger)
		{
			_clientsService = clientsService;
			_expertsService = expertsService;
			_ratesService = ratesService;
			_configuration = configuration;
			_logger = logger;
		}
		public async Task<List<CallPrice>> CalculateEachCallPrice()
		{
			_logger.LogInformation("Get the expert data");
			var experts = await _expertsService.GetExperts();
			var clients = await _clientsService.GetClients();
			var rates = await _ratesService.GetRates();

			var callPrices = new List<CallPrice>();

			foreach (var expert in experts)
			{
				var expertCalls = _expertsService.GetCallsByClient(expert.Calls);

				foreach (var group in expertCalls)
				{
					var client = group.Key;
					var clientCalls = group.Value;
					_logger.LogInformation($"Get the client data of client - {client} where the expert is - {expert.Name}");
					var singleClient = clients.FirstOrDefault(allClient => allClient.Name == client);
					var discountList = singleClient?.Discounts?.ToList();

					bool firstCall = true;
					foreach (var call in clientCalls)
					{
						var duration = call.Duration;
						var price = 0m;
						if (ShouldApplyOneHourAgreement(discountList))
						{
							duration = RoundCallDuration(duration, true);
						}
						else
						{
							duration = RoundCallDuration(duration);
						}

						if (!firstCall && ShouldApplyFollowDiscount(discountList))
						{
							
							decimal discountedRate = expert.HourlyRate * (100 - _configuration.GetValue<decimal>("DiscountPercentage"))/100;
							price = CalculatePrice(duration, discountedRate);
						}
						else
						{
							price = CalculatePrice(duration, expert.HourlyRate);
						}
						_logger.LogInformation($"Get the target currency {expert.Currency} value for client {client} and expert {expert.Name}");
						price =  _ratesService.GetTargetCurrencyValue(rates,_configuration.GetValue<string>("SourceCurrency"), expert.Currency, price);
						var callPriceDto = new CallPrice
						{
							Client = client,
							Expert = expert.Name,
							Price = price
						};

						callPrices.Add(callPriceDto);
						firstCall = false;
					}
				}
			}

			return callPrices;
		}

		private decimal CalculatePrice(int duration, decimal hourlyRate)
		{
			decimal price = 0m;
			price = ((decimal)duration / 60) * hourlyRate;

			return Math.Round(price, 2);
		}

		private int RoundCallDuration(int duration, bool isOneHourAgreement = false)
		{
			if (isOneHourAgreement && duration > 30 && duration < 60)
			{
				const int duration45 = 45;
				return duration45;
			}
			if (duration <= 60)
			{
				return (int)Math.Ceiling(duration / 30.0) * 30;
			}
			else
			{
				return (int)Math.Ceiling((duration - 60) / 15.0) * 15 + 60;
			}
		}
		private bool ShouldApplyOneHourAgreement(List<string> discountList)
		{
			return discountList != null && discountList.Contains(_configuration.GetSection("Discounts:1HourAgreement").Value ?? DISCOUNTONEHOURAGREEMENT);
		}
		private bool ShouldApplyFollowDiscount(List<string> discountList)
		{
			return discountList != null && discountList.Contains(_configuration.GetSection("Discounts:Follow").Value ?? DISCOUNTFOLLOW);
		}
	}

}
