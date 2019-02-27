namespace Simpli.SEO.Web.Services
{
	using System.Linq;
	using System.Threading.Tasks;
	using Microsoft.Extensions.Options;
	using Models;

	public class SearchService : ISearchService
	{
		private readonly AppOptions _appOptions;

		private readonly ISearchExecutor _searchExecutor;

		public SearchService(ISearchExecutor searchExecutor, IOptions<AppOptions> appOptions)
		{
			_searchExecutor = searchExecutor;
			_appOptions = appOptions.Value;
		}

		public async Task<SearchResult> PerformSearchAsync(SearchRequestModel searchModel)
		{
			var config = _appOptions.SearchSources.SingleOrDefault(ss => ss.Matches(searchModel.SearchSource));

			if (config == null)
			{
				return new SearchResult
				{
					Success = false,
					Reason = FailureReason.NotSupported
				};
			}

			var results = await _searchExecutor.DoSearchAsync(new SearchExecutionModel
			{
				SearchUrl = config.SearchUrl,
				ResultXPathExpression = config.ResultXPathExpression,
				ResultHeaderXPathExpression = config.ResultHeaderXPathExpression,
				Query = searchModel.Query
			});

			var matchingItems = results
				.SearchResultItems
				.Where(i => i.Attributes["href"].Contains(searchModel.UrlPartMatch))
				.ToList();

			return new SearchResult
			{
				SearchResultItems = matchingItems
			};
		}
	}
}