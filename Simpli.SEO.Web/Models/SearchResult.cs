namespace Simpli.SEO.Web.Models
{
	using System.Collections.Generic;
	using Microsoft.EntityFrameworkCore.Internal;

	public class SearchResult
	{
		public List<int> Matches { get; set; }

		public bool HasResults => Matches.Any();
	}
}