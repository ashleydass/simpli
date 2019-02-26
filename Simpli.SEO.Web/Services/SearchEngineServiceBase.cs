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
				.Select((n, i) => new
				{
					Rank = i + 1,
					Url = n.Attributes["href"]?.Value
				})
				.Where(r => r.Url.Contains(requestModel.UrlInSearchResults) && r.Rank <= 100)
				.Select(r => r.Rank);

			return new SearchResult
			{
				Matches = matches.ToList()
			};
		}

		protected abstract string ResultXPathExpression { get; }

		protected abstract string SearchUrl(string query);

		public bool Uses(SearchEngineEnum searchEngine) => searchEngine == SearchEngine;
	}
}