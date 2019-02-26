namespace Simpli.SEO.Web.Models
{
	using System;

	public class SearchConfig
	{
		public string Source { get; set; }

		public string SearchUrl { get; set; }

		public string ResultXPathExpression { get; set; }

		public string ResultHeaderXPathExpression { get; set; }

		public bool Matches(string searchSource)
		{
			return Source.Equals(searchSource.Trim(), StringComparison.InvariantCultureIgnoreCase);
		}
	}
}