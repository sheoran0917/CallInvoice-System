using Arbolus.CallInvoice.Models;

namespace Arbolus.CallInvoice.BusinessLogic.Services
{
    public interface IExpertsService
    {
        /// <summary>
        /// Get all the expert data from the endpoint.
        /// </summary>
        /// <returns></returns>
        Task<List<Expert>> GetExperts();

        /// <summary>
        /// Group all calls by client
        /// </summary>
        /// <param name="calls"></param>
        /// <returns></returns>
        Dictionary<string, List<Call>> GetCallsByClient(List<Call> calls);

	}
}
