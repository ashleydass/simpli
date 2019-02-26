namespace Simpli.SEO.Web.Services
{
	using System.Threading.Tasks;
	using Models;

	public interface ISearchExecutor
	{
		Task<SearchResult> DoSearchAsync(SearchExecutionModel searchExecutionModel);
	}
}