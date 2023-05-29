using Arbolus.CallInvoice.BusinessLogic.Services;
using Arbolus.CallInvoice.BusinessLogic;
using Arbolus.CallInvoice.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;

namespace Arbolus.CallInvoice.Tests.BusinessLogic
{
	public class PriceCalculatorTests
	{
		private readonly Mock<IExpertsService> _expertsServiceMock;
		private readonly Mock<IClientsService> _clientsServiceMock;
		private readonly Mock<IRatesService> _ratesServiceMock;
		private readonly Mock<IConfiguration> _configurationMock;
		private readonly Mock<ILogger<PriceCalculator>> _loggerMock;

		public PriceCalculatorTests()
		{
			_expertsServiceMock = new Mock<IExpertsService>();
			_clientsServiceMock = new Mock<IClientsService>();
			_ratesServiceMock = new Mock<IRatesService>();
			_configurationMock = new Mock<IConfiguration>();
			_loggerMock = new Mock<ILogger<PriceCalculator>>();
		}

		private void SetupDefaultMocks()
		{
			var experts = new List<Expert> { new Expert { Name = "Test-Expert1", HourlyRate = 50m, Currency = "USD" } };
			_expertsServiceMock.Setup(service => service.GetExperts()).ReturnsAsync(experts);


			var expertCalls = new Dictionary<string, List<Call>> { { "Test-Client1", new List<Call> { new Call { Duration = 45 } } } };
			_expertsServiceMock.Setup(service => service.GetCallsByClient(It.IsAny<List<Call>>())).Returns(expertCalls);

			//var client = new List<Client> { new Client { Name = "Test-Client1", Discounts = new List<string>() } };
			//_clientsServiceMock.Setup(service => service.GetClients()).ReturnsAsync(client);

			var configuration = TestHelper.GetConfiguration();
			_configurationMock.Setup(config => config.GetSection("Discounts:1HourAgreement")).Returns(configuration.GetSection("Discounts:1HourAgreement"));
			_configurationMock.Setup(config => config.GetSection("SourceCurrency")).Returns(configuration.GetSection("SourceCurrency"));

			var rates = new Rate
			{
				USD = new Dictionary<string, decimal> { { "EUR", 0.8m } }
			};
			_ratesServiceMock.Setup(service => service.GetRates()).ReturnsAsync(rates);
			_ratesServiceMock.Setup(service => service.GetTargetCurrencyValue(rates, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<decimal>()))
				.Returns((Rate rate, string sourceCurrency, string targetCurrency, decimal value) => value);
		}

		[Fact]
		public async Task CalCulateCallPricesSuccessWithOutDiscounts()
		{
			// Arrange
			SetupDefaultMocks();

			var client = new List<Client> { new Client { Name = "Test-Client1", Discounts = new List<string>() } };
			_clientsServiceMock.Setup(service => service.GetClients()).ReturnsAsync(client);

			var priceCalulator = new PriceCalculator(_clientsServiceMock.Object, _expertsServiceMock.Object, _ratesServiceMock.Object, _configurationMock.Object, _loggerMock.Object);

			// Act
			var result = await priceCalulator.CalculateEachCallPrice();

			// Assert
			Assert.NotNull(result);
			Assert.Single(result);

			var callPrice = result.First();
			Assert.Equal("Test-Client1", callPrice.Client);
			Assert.Equal("Test-Expert1", callPrice.Expert);
			Assert.Equal(50m, callPrice.Price);
		}

		[Fact]
		public async Task CalCulateCallPricesSuccessWithDiscounts()
		{
			// Arrange
			SetupDefaultMocks();

			var clientWithDiscounts = new List<Client> { new Client { Name = "Test-Client1", Discounts = new List<string>() { "1 hour agreement" } } };
			_clientsServiceMock.Setup(service => service.GetClients()).ReturnsAsync(clientWithDiscounts);

			var priceCalulator = new PriceCalculator(_clientsServiceMock.Object, _expertsServiceMock.Object, _ratesServiceMock.Object, _configurationMock.Object, _loggerMock.Object);
			// Act
			var result = await priceCalulator.CalculateEachCallPrice();

			// Assert
			Assert.NotNull(result);
			Assert.Single(result);

			var callPrice = result.First();
			Assert.Equal("Test-Client1", callPrice.Client);
			Assert.Equal("Test-Expert1", callPrice.Expert);
			Assert.Equal(37.50m, callPrice.Price);
		}
	}
}
