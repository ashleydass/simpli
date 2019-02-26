namespace Simpli.SEO.Web.Controllers
{
	using System.Linq;
	using System.Threading.Tasks;
	using Dtos;
	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Mvc;
	using Models;
	using Services;

	[Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
	{
		private readonly ISearchService _searchService;

		public SearchController(ISearchService searchService)
		{
			_searchService = searchService;
		}

		[HttpGet("")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SearchResultDto))]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status501NotImplemented)]
		public async Task<ActionResult<SearchResultDto>> GetAsync([FromQuery]SearchRequestModel requestModel)
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
				case FailureReason.NoResults:
					return NotFound();
				default:
					return new StatusCodeResult(500);
			}
		}
	}


}