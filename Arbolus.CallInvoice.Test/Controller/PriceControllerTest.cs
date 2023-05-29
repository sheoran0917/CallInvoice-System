using Moq;
using Arbolus.CallInvoice.BusinessLogic;
using Arbolus.CallInvoice.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.TestHost;
using System.Net;
using Newtonsoft.Json;

namespace Arbolus.CallInvoice.Tests.Controller
{
	public class PriceControllerTest : IAsyncLifetime
	{
        private readonly Mock<IPriceCalculator> _priceCalculatorMock = new();
		private HttpClient _httpClient = null;

		[Fact]
        public async Task GetPrices_HappyPath()
        {
            // Arrange

            var expectedCallPrices = new List<CallPrice> { new CallPrice { Client = "Test Client", Expert="Test Expert", Price = 10m }, new CallPrice { Client = "Test Client1", Expert = "Test Expert1", Price = 10m } };
			_priceCalculatorMock.Setup(calculator => calculator.CalculateEachCallPrice()).ReturnsAsync(expectedCallPrices);

			var response = await _httpClient.GetAsync($"api/price/CallPrices");
			response.EnsureSuccessStatusCode();
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
			var returnedJson = await response.Content.ReadAsStringAsync();

			// Act
           var returendData = JsonConvert.DeserializeObject<List<CallPrice>>(returnedJson);
			// Assert
			Assert.NotNull(returendData);
			bool areEqual = expectedCallPrices.SequenceEqual(returendData, new CallPriceComparer());
			Assert.True(areEqual);
        }
		public async Task InitializeAsync()
		{
			var hostBuilder = Program.CreateHostBuilder(new string[0]).ConfigureWebHost(webHostBuilder =>
			{
				webHostBuilder.UseTestServer();
			})
			.ConfigureServices((_, services) =>
			{
				services.AddSingleton(_priceCalculatorMock.Object);
			});

			var host = await hostBuilder.StartAsync();
			_httpClient = host.GetTestClient();
		}

		public Task DisposeAsync()
		{
			return Task.CompletedTask;
		}
	}
}
