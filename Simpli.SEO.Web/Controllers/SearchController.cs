namespace Simpli.SEO.Web.Controllers
{
	using System.Threading.Tasks;
	using Dtos;
	using Extensions;
	using Microsoft.AspNetCore.Mvc;
	using Models;
	using Services;

	[Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
	{
		private readonly ISearchServiceFactory _searchServiceFactory;

		public SearchController(ISearchServiceFactory searchServiceFactory)
		{
			_searchServiceFactory = searchServiceFactory;
		}

		[HttpGet("")]
		public async Task<SearchResultDto> Get([FromQuery]SearchResultModel requestModel)
		{
			var searchService = _searchServiceFactory.GetSearchService(requestModel.SearchEngine);

			var result = await searchService.PerformSearchAsync(requestModel);

			return result.AsDto();
		}
	}

	
}