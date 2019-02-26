namespace Simpli.SEO.Web.Services
{
	using System.Threading.Tasks;
	using Models;

	public interface ISearchService
	{
		Task<SearchResult> PerformSearchAsync(SearchRequestModel searchModel);
	}
}