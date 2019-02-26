namespace Simpli.SEO.Web.Extensions
{
	using System.Linq;
	using Dtos;
	using Models;

	public static class SearchResultExtensions
	{
		public static SearchResultDto AsDto(this SearchResult searchResult)
		{
			return new SearchResultDto
			{
				Matches = searchResult.SearchResultItems
					.Select(i => i.Rank)
					.ToList()
			};
		}
	}
}
