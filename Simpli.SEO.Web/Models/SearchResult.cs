namespace Simpli.SEO.Web.Models
{
	using System.Collections.Generic;
	using HtmlAgilityPack;

	public class SearchResult
	{
		public IEnumerable<int> Matches { get; set; }
	}
}