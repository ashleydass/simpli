namespace Simpli.SEO.Web.Services
{
	using Models;

	public class BingSearchEngineService : SearchEngineServiceBase
	{
		public override SearchEngineEnum SearchEngine { get; } = SearchEngineEnum.Bing;

		protected override string ResultXPathExpression { get; } = @"//ol[@id='b_results']/li[@class='b_algo']/h2/a";

		protected override string SearchUrl(string query) => $"https://www.bing.com/search?q={query}";
	}
}