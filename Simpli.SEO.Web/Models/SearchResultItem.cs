namespace Simpli.SEO.Web.Models
{
	using System.Collections.Generic;

	public class SearchResultItem
	{
		public int Rank { get; set; }

		public Dictionary<string, string> Attributes { get; set; }

		public string Label { get; set; }
	}
}