namespace Simpli.SEO.Web.Dtos
{
	using System;
	using System.Collections.Generic;

	public class SearchResultDto
	{
		public List<int> Matches { get; set; }

		public DateTime Timestamp { get; set; } = DateTime.Now;
	}
}
