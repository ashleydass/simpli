namespace Simpli.SEO.Web.Models
{
	public class SearchExecutionModel
	{
		public string SearchUrl { get; set; }

		public string Query { get; set; }

		public string ResultXPathExpression { get; set; }

		public string ResultHeaderXPathExpression { get; set; }
	}
}