namespace Simpli.SEO.Web.Services
{
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using Models;

	public interface ISearchService
	{
		List<string> GetAvailableSearchSources();

		Task<SearchResult> PerformSearchAsync(SearchRequestModel searchModel);
	}
}