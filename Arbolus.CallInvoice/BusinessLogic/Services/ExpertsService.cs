using Arbolus.CallInvoice.Models;
using Arbolus.CallInvoice.Common;
using Microsoft.Extensions.Configuration;

namespace Arbolus.CallInvoice.BusinessLogic.Services
{
    public class ExpertsService : IExpertsService
	{
		private readonly ILogger<ClientService> _logger;
		private readonly IConfiguration _configuration;
		private readonly DataRetriever _dataRetriever;

		public ExpertsService(IConfiguration configuration, ILogger<ClientService> logger, DataRetriever dataRetriever)
		{
			_configuration = configuration;
			_logger = logger;
			_dataRetriever = dataRetriever;
		}

		public Dictionary<string, List<Call>> GetCallsByClient(List<Call> calls)
		{
			return calls.GroupBy(call => call.Client).ToDictionary(group => group.Key, group => group.ToList());
		}

		public async Task<List<Expert>> GetExperts()
        {
			try
			{
				var expertData = await _dataRetriever.GetData<ExpertData>(_configuration.GetSection("EndPoints:ExpertsEndpoint").Value ?? "");
				return expertData.Experts;
			}
			catch (Exception ex)
			{
				throw new Exception("Error while getting experts.", ex);
			}
		}
    }
}
