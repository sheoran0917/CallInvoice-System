using Arbolus.CallInvoice.Models;
using Arbolus.CallInvoice.Common;

namespace Arbolus.CallInvoice.BusinessLogic.Services
{
    public class ClientService : IClientsService
    {
        private readonly ILogger<ClientService> _logger;
        private readonly IConfiguration _configuration;
        private readonly DataRetriever _dataRetriever;

        public ClientService(IConfiguration configuration, ILogger<ClientService> logger, DataRetriever dataRetriever)
        {
            _configuration = configuration;
            _logger = logger;
            _dataRetriever = dataRetriever;
        }

		public async Task<List<Client>> GetClients()
        {
            try
            {
                var clientData = await _dataRetriever.GetData<ClientData>(_configuration.GetSection("EndPoints:ClientEndpoint").Value ?? "");
                return clientData.Clients;
            }
            catch (Exception ex)
            {
                throw new Exception("Error while getting clients.", ex);
            }
        }
    }
}
