using Newtonsoft.Json;

namespace Arbolus.CallInvoice.Common
{
	public class DataRetriever
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly ILogger<DataRetriever> _logger;
		private readonly IConfiguration _configuration;
		private readonly string _baseUri;

		public DataRetriever(IConfiguration configuration, IHttpClientFactory httpClientFactory, ILogger<DataRetriever> logger)
		{
			_configuration = configuration;
			_httpClientFactory = httpClientFactory;
			_logger = logger;
			_baseUri = _configuration.GetValue<string>("BaseUri");
		}

		public async Task<T> GetData<T>(string endpoint)
		{
			try
			{
				if(string.IsNullOrEmpty(endpoint))
				{
					_logger.LogError($"No endpoint in appsetting available for {typeof(T).Name} and {nameof(endpoint)}");
					throw new ArgumentNullException($"No endpoint in appsetting available for {typeof(T).Name} and {nameof(endpoint)}");
				}
				var client = _httpClientFactory.CreateClient();
				var requestUri = $"{_baseUri}{endpoint}";
				_logger.LogInformation($"Getting the result of {typeof(T).Name} from URL {requestUri}");
				var response = await client.GetAsync(requestUri);
				response.EnsureSuccessStatusCode();
				var jsonContent = await response.Content.ReadAsStringAsync();

				var data = JsonConvert.DeserializeObject<T>(jsonContent);

				if (data == null)
				{
					throw new Exception($"No {typeof(T).Name} data found.");
				}
				_logger.LogInformation($"Data received for {typeof(T).Name}");
				return data;
			}
			catch (HttpRequestException ex)
			{
				_logger.LogError($"Error while making an HTTP request to retrieve {typeof(T).Name} data. Error: {ex.Message}");
				throw new Exception($"Error while retrieving {typeof(T).Name} data.", ex);
			}
			catch (JsonException ex)
			{
				_logger.LogError($"Error while deserializing {typeof(T).Name} data. Error: {ex.Message}");
				throw new Exception($"Error while deserializing {typeof(T).Name} data.", ex);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Error while getting {typeof(T).Name} data. Error: {ex.Message}");
				throw new Exception($"Error while getting {typeof(T).Name} data.", ex);
			}
		}
	}
}
