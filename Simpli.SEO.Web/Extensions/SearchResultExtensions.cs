namespace Simpli.SEO.Web.Extensions
{
	using Dtos;
	using Models;

	public static class SearchResultExtensions
	{
		public static SearchResultDto AsDto(this SearchResult searchResult)
		{
			return new SearchResultDto
			{
				Matches = searchResult.Matches
			};
		}
	}
}
