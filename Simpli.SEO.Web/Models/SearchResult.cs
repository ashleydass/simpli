namespace Simpli.SEO.Web.Models
{
	using System;
	using System.Collections.Generic;

	public class SearchResult
	{
		public bool Success { get; set; } = true;

		public FailureReason Reason { get; set; }

		public List<SearchResultItem> SearchResultItems { get; set; }
	}
}