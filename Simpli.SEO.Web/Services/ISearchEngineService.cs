namespace Simpli.SEO.Web.Services
{
	using System.Threading.Tasks;
	using Models;

	public interface ISearchEngineService
	{
		SearchEngineEnum SearchEngine { get; }

		Task<SearchResult> PerformSearchAsync(SearchResultModel requestModel);

		bool Uses(SearchEngineEnum searchEngine);
	}
}