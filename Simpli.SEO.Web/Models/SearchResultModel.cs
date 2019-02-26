namespace Simpli.SEO.Web.Models
{
	public class SearchResultModel
	{
		public SearchEngineEnum SearchEngine { get; set; }

		public string Query { get; set; }

		public string UrlInSearchResults { get; set; }
	}

	public enum SearchEngineEnum
	{
		Google,
		Bing
	}
}