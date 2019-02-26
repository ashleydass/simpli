namespace Simpli.SEO.Web.Services
{
	using System.Linq;
	using System.Threading.Tasks;
	using HtmlAgilityPack;
	using Models;

	public class SearchExecutor : ISearchExecutor
	{
		public async Task<SearchResult> DoSearchAsync(SearchExecutionModel searchExecutionModel)
		{
			var web = new HtmlWeb();
			var htmlDoc = await web.LoadFromWebAsync(string.Format(searchExecutionModel.SearchUrl, searchExecutionModel.Query));

			var items = htmlDoc
				.DocumentNode
				.SelectNodes(searchExecutionModel.ResultXPathExpression)
				.Select((n, i) => new SearchResultItem
				{
					Rank = i + 1,
					Attributes = n.Attributes.ToDictionary(a => a.Name, a => a.Value),
					Label = n.SelectSingleNode(searchExecutionModel.ResultHeaderXPathExpression).InnerText
				});

			return new SearchResult
			{
				SearchResultItems = items.ToList()
			};
		}
	}
}