namespace Simpli.SEO.Web.Services
{
	using System.Threading.Tasks;
	using Models;

	public class GoogleSearchEngineService : SearchEngineServiceBase
	{
		public override SearchEngineEnum SearchEngine { get; } = SearchEngineEnum.Google;

		protected override string ResultXPathExpression { get; } = "";

		protected override string SearchUrl(string query) => $"https://www.google.com/search?q={query}";
	}
}