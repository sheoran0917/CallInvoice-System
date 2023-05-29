using Arbolus.CallInvoice.BusinessLogic;
using Arbolus.CallInvoice.Models;
using Microsoft.AspNetCore.Mvc;

namespace Arbolus.CallInvoice.Controllers
{
    [ApiController]
	[Route("api/[controller]")]
	public class PriceController : Controller
	{
		private readonly IPriceCalculator _priceCalculator;
		public PriceController(IPriceCalculator priceCalculator)
		{
			_priceCalculator = priceCalculator;
		}

		[HttpGet]
		[Route("callprices")]
		public async Task<ActionResult<IEnumerable<CallPrice>>> GetPrices()
		{
			var callPrices = await _priceCalculator.CalculateEachCallPrice();
			return Ok(callPrices);
		}
	}
}
