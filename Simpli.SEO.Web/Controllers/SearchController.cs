namespace Simpli.SEO.Web.Controllers
{
	using System.Linq;
	using System.Threading.Tasks;
	using Dtos;
	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.Extensions.Logging;
	using Models;
	using Services;

	[Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
	{
		private readonly ISearchService _searchService;

		private readonly ILogger<SearchController> _logger;

		public SearchController(ISearchService searchService, ILogger<SearchController> logger)
		{
			_searchService = searchService;
			_logger = logger;
		}

		[HttpGet("")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SearchResultDto))]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status501NotImplemented)]
		public async Task<ActionResult<SearchResultDto>> GetAsync([FromQuery]SearchRequestModel requestModel)
		{
			_logger.LogInformation("Inside Search Controller");
			try
			{
				var result = await _searchService.PerformSearchAsync(requestModel);

				if (result.Success)
				{
					return new SearchResultDto
					{
						Matches = result.SearchResultItems.Select(i => i.Rank).ToList()
					};
				}

				switch (result.Reason)
				{
					case FailureReason.NotSupported:
						return new StatusCodeResult(501);
					default:
						return new StatusCodeResult(500);
				}
			}
			catch (System.Exception exception)
			{
				_logger.LogError(exception, "An error occured while processing your request.");
				return new StatusCodeResult(500);
			}
		}
	}


}