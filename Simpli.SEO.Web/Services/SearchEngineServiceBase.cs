namespace Simpli.SEO.Web.Services
{
	using System.Linq;
	using System.Threading.Tasks;
	using HtmlAgilityPack;
	using Models;

	public abstract class SearchEngineServiceBase : ISearchEngineService
	{
		public abstract SearchEngineEnum SearchEngine { get; }

		public async Task<SearchResult> PerformSearchAsync(SearchResultModel requestModel)
		{
			var web = new HtmlWeb();
			var htmlDoc = await web.LoadFromWebAsync(SearchUrl(requestModel.Query));

			var matches = htmlDoc
				.DocumentNode
				.SelectNodes(ResultXPathExpression)
				.Select((n, i) => i);

			return new SearchResult
			{
				Matches = matches
			};
		}

		protected abstract string ResultXPathExpression { get; }

		protected abstract string SearchUrl(string query);

		public bool Uses(SearchEngineEnum searchEngine) => searchEngine == SearchEngine;
	}
}