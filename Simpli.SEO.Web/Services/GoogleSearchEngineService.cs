namespace Simpli.SEO.Web.Services
{
	using Models;

	public class GoogleSearchEngineService : SearchEngineServiceBase
	{
		public override SearchEngineEnum SearchEngine { get; } = SearchEngineEnum.Google;

		protected override string ResultXPathExpression { get; } = "//div[@class='g']//a";

		protected override string SearchUrl(string query) => $"https://www.google.com/search?q={query}";
	}
}