using Arbolus.CallInvoice.Models;

namespace Arbolus.CallInvoice.BusinessLogic.Services
{
    public interface IClientsService
    {
        /// <summary>
        /// Get all clients data from the endpoints
        /// </summary>
        /// <returns></returns>
        Task<List<Client>> GetClients();

    }
}
